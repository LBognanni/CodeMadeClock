using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class RectangleShape : IShape
    {
        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string Color { get; set; }

        public bool Smooth { get; set; }

        public RectangleShape()
        {
        }

        public RectangleShape(int left, int top, int width, int height, string color)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Color = color;
        }

        public virtual void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            using (var smoothing = new SmoothingOption(g, Smooth? System.Drawing.Drawing2D.SmoothingMode.AntiAlias : System.Drawing.Drawing2D.SmoothingMode.Default))
            {
                using (var brush = Color.ParseBrush(rect))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }
    }
}