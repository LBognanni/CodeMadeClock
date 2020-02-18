using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmClock : Form
    {
        ClockCanvas _canvas;
        ClockCanvas _renderCanvas;
        ITimer _timer;
        
        public frmClock(string fileName)
        {
            InitializeComponent();
            Size = Properties.Settings.Default.Size;
            Text = "CodeMade Clock";
            ShowInTaskbar = false;
            Size = new Size(256, 256);
            _timer = new ClockTimer();
            var canvas = Canvas.Load(fileName ?? @"democlock.json");
            _canvas = new ClockCanvas(_timer, canvas);
            TopMost = true;
            tsmClose.Image = il24.Images[0];
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
            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();

        }
        protected override void OnResize(EventArgs e)
        {
            _renderCanvas = null;
            base.OnResize(e);
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
            UpdateSize(0.75);
        }

        private void LargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateSize(1.25);
        }

        private void UpdateSize(double multiplier)
        {
            Size = new Size((int)(Size.Width * multiplier), (int)(Size.Height * multiplier));
            Properties.Settings.Default.Size = Size;
            Properties.Settings.Default.Save();
        }
    }
}
