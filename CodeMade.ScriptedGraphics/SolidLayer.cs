﻿using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class SolidLayer : Layer
    {
        private string backgroundColor;
        
        public SolidLayer(string backgroundColor)
        {
            this.backgroundColor = backgroundColor;
        }

        protected override void BeforeTransform(Graphics g, float scaleFactor)
        {
            using (var smoothing = new SmoothingOption(g, System.Drawing.Drawing2D.SmoothingMode.Default))
            {
                using (var brush = new SolidBrush(backgroundColor.ToColor()))
                {
                    g.FillRectangle(brush, g.VisibleClipBounds);
                }
            }
        }
    }
}