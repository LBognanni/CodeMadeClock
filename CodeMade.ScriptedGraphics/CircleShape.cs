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
            X = x;
            Y = y;
            Radius = radius;
            Color = color;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public string Color { get; set; }

        public virtual void Render(Graphics g, float scaleFactor = 1)
        {
            var diameter = Radius * 2 * scaleFactor;
            var left = (X - Radius) * scaleFactor;
            var top = (Y - Radius) * scaleFactor;

            using (var brush = Color.ParseBrush(new RectangleF(left, top, diameter, diameter)))
            {
                g.FillEllipse(brush, left, top, diameter, diameter);
            }
        }
    }
}
