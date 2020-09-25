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

        public frmClock(ISettings settings, SkinPackCollection skinpacks, string skinOverride = null)
        {
            _locationSetter = new LocationSetter(this);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Text = "CodeMade Clock";
            TopMost = true;
            tsmClose.Image = il24.Images[0];
            ShowInTaskbar = false;

            _settings = settings;
            _skinpacks = skinpacks;

            _timer = new ClockTimer();
            LoadSize();
            LoadSkin(skinOverride);

            _debouncedSaveSettings = ((Action)SaveSettings).Debounce(500);
            this.Resize += FrmClock_Resize;
        }

        private void FrmClock_Resize(object sender, EventArgs e)
        {
            _renderCanvas = null;
            OnSaveSettings();
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

        internal string SelectedSkin { get; private set; }

        private void LoadSkin(string skinOverride)
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
                SelectedSkin = skin.Name;
                canvas = skin.Canvas;
            }
            else
            {
                canvas = Canvas.Load(skinOverride);
                SelectedSkin = skinOverride;
            }
            _canvas = new ClockCanvas(_timer, canvas);
            _renderCanvas = null;
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

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings(_skinpacks, _settings);
            form.TopMost = this.TopMost;
            if(form.ShowDialog() == DialogResult.OK)
            {
                LoadSkin(null);
            }
        }
    }
}
