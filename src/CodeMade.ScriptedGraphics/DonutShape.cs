using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    public class DonutShape : CircleShape
    {
        public float InnerRadius { get; set; }

        public DonutShape()
        {
        }

        public override void Render(Graphics g, float scaleFactor = 1)
        {
            using (var path = new GraphicsPath())
            {
                var rect = AddEllipse(path, Radius, scaleFactor);
                AddEllipse(path, InnerRadius, scaleFactor);

                using (var brush = Color.ParseBrush(rect))
                {
                    var state = g.BeginContainer();

                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.FillPath(brush, path);

                    g.EndContainer(state);
                }
            }
        }

        private RectangleF AddEllipse(GraphicsPath path, float radius, float scaleFactor)
        {
            var diameter = radius * 2 * scaleFactor;
            var left = (Position.X - radius) * scaleFactor;
            var top = (Position.Y - radius) * scaleFactor;

            var rect = new RectangleF(left, top, diameter, diameter);
            path.AddEllipse(rect);

            return rect;
        }
    }
}
