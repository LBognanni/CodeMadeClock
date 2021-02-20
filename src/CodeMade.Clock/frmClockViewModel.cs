using CodeMade.Clock.LocationMoving;
using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public class frmClockViewModel : ReactiveObject
    {
        private readonly ISettings _settings;
        private readonly SkinPackCollection _skinpacks;
        private readonly ITimer _timer;
        private readonly ILocationFixer _locationFixer;

        private ClockCanvas _canvas;
        private ClockCanvas _renderCanvas;

        private int _width;
        private int _height;
        private Bitmap _image;
        private Point _location;

        public int Width 
        { 
            get => _width; 
            set => this.RaiseAndSetIfChanged(ref _width, value); 
        }
        public int Height 
        { 
            get => _height; 
            set => this.RaiseAndSetIfChanged(ref _height, value); 
        }
        public Bitmap Image 
        { 
            get => _image; 
            set => this.RaiseAndSetIfChanged(ref _image, value); 
        }
        public Point Location 
        { 
            get => _location; 
            set => this.RaiseAndSetIfChanged(ref _location, value); 
        }

        public frmClockViewModel(ISettings settings, SkinPackCollection skinpacks, ITimer timer, ILocationFixer locationFixer, string skinOverride = null, IScheduler scheduler = null, int throttleTimeInMs = 500)
        {
            _settings = settings;
            _skinpacks = skinpacks;
            _timer = timer;
            _locationFixer = locationFixer;
            scheduler ??= RxApp.MainThreadScheduler;
            LoadSkin(skinOverride);
            LoadSize();

            this.WhenAnyValue(x => x.Width, x => x.Height)
                .Throttle(TimeSpan.FromMilliseconds(throttleTimeInMs))
                .ObserveOn(scheduler)
                .Subscribe(size =>
                {
                    ForceUpdateImage();
                });

            this.WhenAnyValue(x => x.Width, x => x.Height, x => x.Location)
                .Throttle(TimeSpan.FromMilliseconds(throttleTimeInMs))
                .ObserveOn(scheduler)
                .Subscribe(things =>
                {
                    (int w, int h, Point loc) = things;
                    _settings.Size = new Size(w, h);
                    _settings.Location = loc;
                    _settings.Save();
                });

            Observable.Interval(TimeSpan.FromMilliseconds(250))
                .ObserveOn(scheduler)
                .Subscribe(_ =>
                {
                    UpdateImage();
                });
        }

        public void ForceUpdateImage()
        {
            _renderCanvas = null;
            UpdateImage();
        }

        private void LoadSize()
        {
            if (_settings.HasSettings)
            {
                Width = _settings.Size.Width;
                Height = _settings.Size.Height;
                Location = _locationFixer.FixLocation(_settings.Location);
            }
            else
            {
                Width = Height = 256;
            }
        }

        private void UpdateImage()
        {
            var szx = (float)Width / (float)_canvas.Width;
            var szy = (float)Height / (float)_canvas.Height;
            var ratio = Math.Min(szx, szy);

            if (_renderCanvas == null)
            {
                _renderCanvas = _renderCanvas = _canvas.OptimizeFor(ratio);
            }
            _renderCanvas.Update();

            Image = _renderCanvas.Render(ratio);
        }

        public void LoadSkin(string skinOverride)
        {
            Canvas canvas;
            if (string.IsNullOrEmpty(skinOverride) || !File.Exists(skinOverride))
            {
                var skin = _skinpacks.Packs[_settings.SelectedSkinpack]?.Skins
                                .FirstOrDefault(s => s.Name.Equals(_settings.SelectedSkin, StringComparison.OrdinalIgnoreCase));
                if (skin == null)
                {
                    skin = _skinpacks.Packs.First().Value.Skins.First();
                }
                canvas = skin.Canvas;
            }
            else
            {
                canvas = Canvas.Load(skinOverride);
            }
            _canvas = new ClockCanvas(_timer, canvas);
            _renderCanvas = null;
        }


        private void UpdateSize(double multiplier)
        {
            Width = (int)(Width * multiplier);
            Height = (int)(Height * multiplier);
        }

        public void Bigger() => UpdateSize(1.25);
        public void Smaller() => UpdateSize(0.8);

    }
}
