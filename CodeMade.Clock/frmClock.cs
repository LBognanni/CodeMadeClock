using CodeMade.Clock.LocationMoving;
using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmClock : Form, ILocationReceiver
    {
        private ClockCanvas _canvas;
        private ClockCanvas _renderCanvas;
        private ITimer _timer;
        private ISettings _settings;
        private SkinPackCollection _skinpacks;
        private Action _debouncedSaveSettings;
        private LocationSetter _locationSetter;

        public frmClock(string skinOverride = null, ISettings settings = null, SkinPackCollection skinpacks = null)
        {
            _locationSetter = new LocationSetter(this);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Text = "CodeMade Clock";
            TopMost = true;
            tsmClose.Image = il24.Images[0];
            ShowInTaskbar = false;

            _settings = settings ?? Settings.Load(Path.Combine(Application.LocalUserAppDataPath, "settings.json"));
            _skinpacks = skinpacks ?? SkinPackCollection.Load(Path.Combine(Application.LocalUserAppDataPath, "skinpacks"), Path.Combine(Application.StartupPath, "skinpacks"));

            _timer = new ClockTimer();
            LoadSize();
            LoadSkin(skinOverride);

            _debouncedSaveSettings = ((Action)SaveSettings).Debounce(500);
        }

        private void LoadSize()
        {
            if (_settings.HasSettings)
            {
                Size = _settings.Size;
                StartPosition = FormStartPosition.Manual;
                _locationSetter.SetLocation(_settings.Location);
            }
            else
            {
                Size = new Size(256, 256);
            }
        }

        private void LoadSkin(string skinOverride)
        {
            Canvas canvas;
            if (string.IsNullOrEmpty(skinOverride))
            {
                canvas = _skinpacks.Packs[_settings.SelectedSkinpack]?.Skins
                                .FirstOrDefault(s => s.Name.Equals(_settings.SelectedSkin, StringComparison.OrdinalIgnoreCase)).Canvas;
                if (canvas == null)
                {
                    canvas = _skinpacks.Packs.First().Value.Skins.First().Canvas;
                }
            }
            else
            {
                canvas = Canvas.Load(skinOverride);
            }
            _canvas = new ClockCanvas(_timer, canvas);
        }

        protected void SaveSettings()
        {
            _settings.Location = Location;
            _settings.Size = Size;
            _settings.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            ControlBox = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            base.OnLoad(e);
            UpdateImage();
            var timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            _renderCanvas = null;
            base.OnResize(e);

            OnSaveSettings();
        }

        private void OnSaveSettings() => 
            _debouncedSaveSettings?.Invoke();

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, new UIntPtr(WinAPI.HTCAPTION), IntPtr.Zero);
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(this, e.Location);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            OnSaveSettings();
            base.OnLocationChanged(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                return WinAPI.CreateParams(base.CreateParams);
            }
        }

        public IEnumerable<Rectangle> Screens => Screen.AllScreens.Select(s => s.Bounds);

        protected override void OnClick(EventArgs e)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            var szx = (float)Size.Width / (float)_canvas.Width;
            var szy = (float)Size.Height / (float)_canvas.Height;
            var ratio = Math.Min(szx, szy);
            if (_renderCanvas == null)
            {
                _renderCanvas = _renderCanvas = _canvas.OptimizeFor(ratio);
            }
            _renderCanvas.Update();
            WinAPI.SetFormBackground(this, _renderCanvas.Render(ratio));
        }

        private void TsmClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SmallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateSize(0.8);
        }

        private void LargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateSize(1.25);
        }

        private void UpdateSize(double multiplier)
        {
            Size = new Size((int)(Size.Width * multiplier), (int)(Size.Height * multiplier));
            _settings.Size = Size;
            OnSaveSettings();
        }
    }
}
