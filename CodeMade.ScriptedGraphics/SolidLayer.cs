using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class SolidLayer : LayerBase
    {
        private string backgroundColor;
        
        public SolidLayer(string backgroundColor)
        {
            this.backgroundColor = backgroundColor;
        }

        protected override void BeforeTransform(Graphics g)
        {
            using (var brush = new SolidBrush(backgroundColor.ToColor()))
                g.FillRectangle(brush, g.ClipBounds);
        }
    }
}