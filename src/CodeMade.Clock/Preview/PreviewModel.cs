using CodeMade.ScriptedGraphics;
using NodaTime;
using NodaTime.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public class PreviewModel : ReactiveObject, IDisposable, ITimer
    {
        private string _fileToWatch;

        private readonly ObservableAsPropertyHelper<IEnumerable<string>> _files;

        public IEnumerable<string> Files => _files.Value;

        private Canvas _canvas;
        private ClockCanvas _clockCanvas;
        private FileSystemWatcher _fsw;
        private string _log;

        private Image _image;
        private float _scaleFactor;
        private bool _specificTimeEnabled;
        private DateTime _specificTime;
        private int _renderWidth;
        private int _renderHeight;
        private readonly Type[] _knownTypes;
        private BackgroundStyles _backgroundStyle;
        private ObservableAsPropertyHelper<Image> _backgroundImage;
        private ObservableAsPropertyHelper<Color> _backgroundColor;
        private bool _optimize;

        public enum BackgroundStyles
        {
            Checkers,
            White,
            Black
        }

        public string Log
        {
            get => _log;
            set => this.RaiseAndSetIfChanged(ref _log, value);
        }

        public string FileToWatch
        {
            get => _fileToWatch;
            set => this.RaiseAndSetIfChanged(ref _fileToWatch, value);
        }
        private ClockCanvas ClockCanvas
        {
            get => _clockCanvas;
            set => this.RaiseAndSetIfChanged(ref _clockCanvas, value);
        }

        public Image Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }
        public float ScaleFactor
        {
            get => _scaleFactor;
            set => this.RaiseAndSetIfChanged(ref _scaleFactor, value);
        }
        public bool SpecificTimeEnabled
        {
            get => _specificTimeEnabled;
            set => this.RaiseAndSetIfChanged(ref _specificTimeEnabled, value);
        }
        public DateTime SpecificTime
        {
            get => _specificTime;
            set => this.RaiseAndSetIfChanged(ref _specificTime, value);
        }
        public int RenderWidth
        {
            get => _renderWidth;
            set => this.RaiseAndSetIfChanged(ref _renderWidth, value);
        }
        public int RenderHeight
        {
            get => _renderHeight;
            set => this.RaiseAndSetIfChanged(ref _renderHeight, value);
        }
        public bool OptimizePreview
        {
            get => _optimize;
            set => this.RaiseAndSetIfChanged(ref _optimize, value);
        }
        public Image BackgroundImage => _backgroundImage.Value;
        public Color BackgroundColor => _backgroundColor.Value;

        private IEnumerable<string> GetNearbyFiles(string file)
        {
            string path = Path.GetDirectoryName(file);
            return Directory.GetFiles(path, "*.json");
        }

        public BackgroundStyles BackgroundStyle
        { 
            get => _backgroundStyle; 
            set => this.RaiseAndSetIfChanged(ref _backgroundStyle, value); 
        }

        public PreviewModel(string fileToWatch, int renderWidth, int renderHeight, Type []knownTypes)
        {
            _specificTime = DateTime.Today.AddHours(10).AddMinutes(10).AddSeconds(33);
            _renderWidth = renderWidth;
            _renderHeight = renderHeight;
            _knownTypes = knownTypes;
            _fsw = new FileSystemWatcher(Path.GetDirectoryName(fileToWatch));
            var created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(x => _fsw.Created += x, y => _fsw.Created -= y).Select(x => x.EventArgs.FullPath);
            var changed = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(x => _fsw.Changed += x, y => _fsw.Changed -= y).Select(x => x.EventArgs.FullPath);
            var deleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(x => _fsw.Deleted += x, y => _fsw.Deleted -= y).Select(x => x.EventArgs.FullPath);

            var whenFileToWatchChanges = this
                .WhenAnyValue(x => x.FileToWatch)
                .Merge(created)
                .Merge(changed)
                .Merge(deleted)
                .Where(x => File.Exists(x));

            _files = whenFileToWatchChanges
                .Select(GetNearbyFiles)
                .ToProperty(this, x => x.Files);

            whenFileToWatchChanges
                .Where(x => x == FileToWatch)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(ReloadCanvas);

            this.WhenAnyValue(
                    x => x.ClockCanvas, 
                    x => x.ScaleFactor, 
                    x => x.SpecificTime, 
                    x => x.SpecificTimeEnabled,
                    x => x.OptimizePreview)
                .Where(x => (x.Item1 != null && x.Item2 > 0))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => UpdateImage(x.Item1, x.Item2, x.Item5));

            this.WhenAnyValue(x => x.RenderWidth, x => x.RenderHeight).Throttle(TimeSpan.FromMilliseconds(300))
                .Where(x => _canvas != null)
                .Subscribe(x => UpdateScale());

            _backgroundImage = this.WhenAnyValue(x => x.BackgroundStyle).Select(GetBackgroundImage).ToProperty(this, x => x.BackgroundImage);
            _backgroundColor = this.WhenAnyValue(x => x.BackgroundStyle).Select(GetBackgroundColor).ToProperty(this, x => x.BackgroundColor);

            _fsw.EnableRaisingEvents = true;
            FileToWatch = fileToWatch;
            BackgroundStyle = BackgroundStyles.Checkers;

            UpdateImageCommand = ReactiveCommand.Create<MouseEventArgs>(_ => UpdateImage(_clockCanvas, _scaleFactor, _optimize));
        }

        private Image GetBackgroundImage(BackgroundStyles style) => 
            style switch
            {
                BackgroundStyles.Checkers => Properties.Resources.generic_bg,
                _ => null
            };
        private Color GetBackgroundColor(BackgroundStyles style) =>
            style switch
            {
                BackgroundStyles.White => Color.White,
                _ => Color.Black
            };

        public ReactiveCommand<MouseEventArgs, Unit> UpdateImageCommand
        {
            get;
        }

        private void UpdateScale() =>
            ScaleFactor = Math.Min((float)RenderWidth / (float)_canvas.Width, (float)RenderHeight / (float)_canvas.Height);

        private void UpdateImage(ClockCanvas clock, float scaleFactor, bool optimize)
        {
            try
            {
                var oldImg = _image;
                if (optimize)
                {
                    clock = clock.OptimizeFor(scaleFactor);
                }

                clock.Update();
                Image = clock.Render(scaleFactor);
                Log = "OK";
                oldImg?.Dispose();
            }
            catch (Exception ex)
            {
                Log = ex.Message;
                Image = null;
            }
        }

        private void ReloadCanvas(string fileName)
        {
            try
            {
                _canvas = Canvas.Load(_fileToWatch, _knownTypes);
                if (_canvas == null)
                {
                    return;
                }
                ClockCanvas = new ClockCanvas(this, _canvas);
                UpdateScale();

                Log = "OK";
            }
            catch (Exception ex)
            {
                Log = ex.Message;
            }
        }


        public void Dispose()
        {
            _image?.Dispose();
            _fsw?.Dispose();
            _fsw = null;
        }

        public Instant GetTime()
        {
            if (SpecificTimeEnabled)
            {
                return SpecificTime.ToUniversalTime().ToInstant();
            }
            return SystemClock.Instance.GetCurrentInstant();
        }
    }
}

