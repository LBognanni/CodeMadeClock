using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A rectangle
    /// </summary>
    /// <example>
    /// {
    ///     "$type": "RectangleShape",
    ///     "Left": -1.5,
    ///     "Top": -60,
    ///     "Width": 3,
    ///     "Height": 11,
    ///     "Color": "#0008"
    /// }
    /// </example>
    public class RectangleShape : IShape
    {
        /// <summary>
        /// X coordinate of the leftmost side
        /// </summary>
        public float Left { get; set; }
        /// <summary>
        /// Y coordinate of the top side
        /// </summary>
        public float Top { get; set; }
        /// <summary>
        /// Width of the rectangle
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Height of the rectangle
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// Fill color
        /// </summary>
        /// <see cref="Colors"/>
        public string Color { get; set; }

        /// <summary>
        /// True to enable anti-aliasing on this rectangle. Set this to `true` if the rectangle is going to be rotated. 
        /// </summary>
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