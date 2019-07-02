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

        public virtual void Render(Graphics g)
        {
            using (var brush = new SolidBrush(Color.ToColor()))
            {
                var diameter = Radius * 2;
                g.FillEllipse(brush, X - Radius, Y - Radius, diameter, diameter);
            }
        }
    }
}
