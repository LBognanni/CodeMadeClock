using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics
{
    public class RotateRepeatLayer : Layer
    {
        public RotateRepeatLayer()
        {
        }

        public int RepeatCount { get; set; }

        public float RepeatRotate { get; set; }

        private int _repeatCounter;

        public override void Render(Graphics g, float scaleFactor = 1)
        {
            for (_repeatCounter = 0; _repeatCounter < RepeatCount; _repeatCounter++)
            {
                base.Render(g, scaleFactor);
            }
        }

        protected override void ApplyTransform(Graphics g, float scaleFactor)
        {
            base.ApplyTransform(g, scaleFactor);
            g.RotateTransform(RepeatRotate * (float)_repeatCounter, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }
    }
}
