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
        ITimer _timer;

        protected override void OnLoad(EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            base.OnLoad(e);
            UpdateImage();
            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Start();
        }
        protected override void OnResize(EventArgs e)
        {
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
                WinAPI.SendMessage(this.Handle, WinAPI.WM_NCLBUTTONDOWN, new UIntPtr(WinAPI.HTCAPTION), IntPtr.Zero);
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
            _canvas.Update();
            var szx = (float)this.Size.Width / (float)_canvas.Width;
            var szy = (float)this.Size.Height / (float)_canvas.Height;
            WinAPI.SetFormBackground(this, _canvas.Render(Math.Min(szx,szy)));
        }

        public frmClock()
        {
            InitializeComponent();
            _timer = new ClockTimer();
            var canvas = Canvas.Load(@"democlock.json");
            _canvas = new ClockCanvas(_timer, canvas);
            canvas.Save("test.json");
            this.TopMost = true;
        }

    }
}
