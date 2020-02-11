using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class CircleShape : IShape
    {
        public CircleShape()
        {
        }

        public CircleShape(float x, float y, float radius, string color)
        {
            Position = new Vertex(x, y);
            Radius = radius;
            Color = color;
        }

        public Vertex Position { get; set; }
        public float Radius { get; set; }
        public string Color { get; set; }

        public virtual void Render(Graphics g, float scaleFactor = 1)
        {
            var diameter = Radius * 2 * scaleFactor;
            var left = (Position.X - Radius) * scaleFactor;
            var top = (Position.Y - Radius) * scaleFactor;

            RenderCircle(g, diameter, left, top);
        }

        protected virtual void RenderCircle(Graphics g, float diameter, float left, float top)
        {
            using (var brush = Color.ParseBrush(new RectangleF(left, top, diameter, diameter)))
            {
                g.FillEllipse(brush, left, top, diameter, diameter);
            }
        }
    }
}
