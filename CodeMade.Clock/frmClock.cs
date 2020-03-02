using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmClock : Form
    {
        private ClockCanvas _canvas;
        private ClockCanvas _renderCanvas;
        private ITimer _timer;
        private Settings _settings;
        private Action _debouncedSaveSettings;
        
        public frmClock(string fileName)
        {
            InitializeComponent();
            
            _settings = Settings.Load("settings.json");
            if (_settings.HasSettings)
            {
                Size = _settings.Size;
                StartPosition = FormStartPosition.Manual;
                LoadLocation(_settings.Location);
            }
            else
            {
                Size = new Size(256, 256);
            }

            Text = "CodeMade Clock";
            ShowInTaskbar = false;
            _timer = new ClockTimer();
            var canvas = Canvas.Load(fileName ?? @"democlock.json");
            _canvas = new ClockCanvas(_timer, canvas);
            TopMost = true;
            tsmClose.Image = il24.Images[0];

            _debouncedSaveSettings = ((Action)SaveSettings).Debounce(1500);
        }

        private void LoadLocation(Point location)
        {
            var closestScreen = Screen.PrimaryScreen;
            var closestDistance = FindCenter(Screen.PrimaryScreen.Bounds);
            var clockRect = new Rectangle(location, Size);
            var clockPt = FindCenter(clockRect);
            var intersects = new List<Screen>();

            foreach(var screen in Screen.AllScreens)
            {
                if (screen.Bounds.Contains(clockRect))
                {
                    Location = location;
                    return;
                }
                else if (screen.Bounds.IntersectsWith(clockRect))
                {
                    intersects.Add(screen);
                }
            }

            switch (intersects.Count)
            {
                case 0:
                    // Not on any screen - move in the closest one
                    var screens = Screen.AllScreens.Select(s => (s, FindDistanceSquared(s.Bounds, clockPt)));
                    MoveToScreen(location, screens.OrderBy(s => s.Item2).First().s);
                    break;
                case 1:
                    // Intersects one screen - move to it
                    MoveToScreen(location, intersects[0]);
                    break;
                default:
                    // Intersect multiple screens - this might be fine (in between two side-by-side screens)
                    Location = location;
                    break;
            }
        }

        private void MoveToScreen(Point location, Screen screen)
        {
            if(location.X < screen.Bounds.Left)
            {
                location.X = screen.Bounds.Left;
            }
            else if(location.X + Width > screen.Bounds.Right)
            {
                location.X = screen.Bounds.Right - Width;
            }

            if(location.Y < screen.Bounds.Top)
            {
                location.Y = screen.Bounds.Top;
            }
            else if(location.Y + Height > screen.Bounds.Bottom)
            {
                location.Y = screen.Bounds.Bottom - Height;
            }

            Location = location;
        }

        private Point FindCenter(Rectangle bounds)
        {
            return new Point(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
        }

        private decimal FindDistanceSquared(Rectangle bounds, Point pt)
        {
            var pt2 = FindCenter(bounds);
            return (pt.X * pt2.X) + (pt.Y * pt2.Y);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            base.OnLoad(e);
            UpdateImage();
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            _renderCanvas = null;
            base.OnResize(e);

            if (_debouncedSaveSettings != null)
            {
                _debouncedSaveSettings();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, new UIntPtr(WinAPI.HTCAPTION), IntPtr.Zero);
            }
            else if(e.Button == MouseButtons.Right)
            {
                contextMenu.Show(this, e.Location);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (_debouncedSaveSettings != null)
            {
                _debouncedSaveSettings();
            }
            base.OnLocationChanged(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                return WinAPI.CreateParams(base.CreateParams);
            }
        }

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
            _settings.Save();
        }
    }
}
