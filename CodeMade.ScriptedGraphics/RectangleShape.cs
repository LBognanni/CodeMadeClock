using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class RectangleShape : IShape
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }

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

        public void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            using (var smoothing = new SmoothingOption(g, System.Drawing.Drawing2D.SmoothingMode.Default))
            {
                using (var brush = Color.ParseBrush(rect))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }
    }
}