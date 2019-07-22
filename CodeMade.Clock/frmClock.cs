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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            base.OnLoad(e);
            UpdateImage();
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

        private void UpdateImage()
        {
            _secondHand.TransformRotate = _timer.GetTime().Second * 6;
            WinAPI.SetFormBackground(this, _canvas.Render());
        }

        public frmClock()
        {
            InitializeComponent();
            _timer = new ClockTimer();
            _canvas = new ClockCanvas(_timer, 255, 255, "#FFF0");

            _canvas.Layers.Add(new GaussianBlurLayer(4));
            _canvas.Add(new CircleShape(130, 130, 110, "#0008"));

            _canvas.Layers.Add(new Layer());
            _canvas.Add(new CircleShape(128, 128, 110, "#e0e0e0"));

            _secondHand = new Layer();
            _secondHand.Shapes.Add(new RectangleShape(-5, -50, 10, 55, "#000"));
            _secondHand.Offset = new Vertex(128, 128);

            _canvas.Add(_secondHand);

            this.TopMost = true;
        }

    }
}
