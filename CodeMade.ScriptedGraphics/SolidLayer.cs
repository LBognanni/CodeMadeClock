using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class SolidLayer : Layer
    {
        public string BackgroundColor { get; set; }
        
        public SolidLayer(string backgroundColor)
        {
            this.BackgroundColor = backgroundColor;
        }

        protected override void BeforeTransform(Graphics g, float scaleFactor)
        {
            using (var smoothing = new SmoothingOption(g, System.Drawing.Drawing2D.SmoothingMode.Default))
            {
                using (var brush = new SolidBrush(BackgroundColor.ToColor()))
                {
                    g.FillRectangle(brush, g.VisibleClipBounds);
                }
            }
            base.BeforeTransform(g, scaleFactor);
        }
    }
}