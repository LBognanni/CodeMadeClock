using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Layer _secondHand;

        protected override void OnLoad(EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None | FormBorderStyle.FixedSingle;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            base.OnLoad(e);
            UpdateImage();
            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 100;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private const int WM_NC_HITTEST = 0x0084;
        private const int HT_CAPTION = 0x02;
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == WM_NC_HITTEST)
            {
                m.Result = new IntPtr(HT_CAPTION);
                return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                return WinAPI.CreateParams(base.CreateParams);
            }
        }


        class ClockTimer : ITimer
        {
            public DateTime GetTime()
            {
                return DateTime.Now;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            UpdateImage();
        }

        int _lastSecond = -1;
        private void UpdateImage()
        {
            var second = _timer.GetTime().Second;
            if (_lastSecond != second)
            {
                _lastSecond = second;
                _secondHand.TransformRotate = second * 6;
                WinAPI.SetFormBackground(this, _canvas.Render());
            }
        }

        public frmClock()
        {
            InitializeComponent();
            _timer = new ClockTimer();
            var canvas = new Canvas(255, 255, "#FFF0");
            canvas.Layers.Add(new GaussianBlurLayer(4));
            canvas.Add(new CircleShape(130, 130, 110, "#0008"));

            canvas.Layers.Add(new Layer());
            canvas.Add(new CircleShape(128, 128, 110, "#e0e0e0"));

            _secondHand = new Layer();
            _secondHand.Shapes.Add(new RectangleShape(-5, -50, 10, 55, "#000"));
            _secondHand.Offset = new Vertex(128, 128);

            canvas.Add(_secondHand);

            _canvas = new ClockCanvas(_timer, canvas);
            this.TopMost = true;
        }

    }
}
