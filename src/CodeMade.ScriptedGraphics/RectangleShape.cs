using CodeMade.ScriptedGraphics.Colors;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        /// <see cref="CodeMade.ScriptedGraphics.Colors.Colors"/>
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

        /// <summary>
        /// The radius of the corners of the rectangle. If set to 0, the rectangle will have sharp corners.
        /// If set to a value greater than 0, the rectangle will have rounded corners, and the "Smooth" option will also implicitly be true.
        /// </summary>
        public float CornerRadius { get; set; } = 0;

        private GraphicsPath GetPath(float scaleFactor)
        {
            var path = new GraphicsPath();
            path.AddArc(Left * scaleFactor, Top * scaleFactor, CornerRadius * scaleFactor, CornerRadius * scaleFactor, 180, 90);
            path.AddArc((Left + Width - CornerRadius) * scaleFactor, Top * scaleFactor, CornerRadius * scaleFactor, CornerRadius * scaleFactor, 270, 90);
            path.AddArc((Left + Width - CornerRadius) * scaleFactor, (Top + Height - CornerRadius) * scaleFactor, CornerRadius * scaleFactor, CornerRadius * scaleFactor, 0, 90);
            path.AddArc(Left * scaleFactor, (Top + Height - CornerRadius) * scaleFactor, CornerRadius * scaleFactor, CornerRadius * scaleFactor, 90, 90);
            path.CloseFigure();
            return path;
        }

        public virtual void Render(Graphics g, float scaleFactor)
        {
            var rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);
            var slightlyBiggerRect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor * 1.1f, Height * scaleFactor * 1.1f);

            using (var smoothing = new SmoothingOption(g, Smooth || (CornerRadius > 0) ? SmoothingMode.AntiAlias : SmoothingMode.Default))
            {
                var brushes = Color.ParseBrush(Smooth || (CornerRadius > 0) ? slightlyBiggerRect : rect);
                try
                {
                    if (CornerRadius == 0)
                    {
                        foreach (var brush in brushes)
                        {
                            brush.Match(
                                b => g.FillRectangle(b, rect),
                                r => g.FillRegion(r.Color, r.Region));
                        }
                    }
                    else
                    {
                        var path = GetPath(scaleFactor);
                        foreach (var brush in brushes)
                        {
                            brush.Match(
                                b => g.FillPath(b, path),
                                r => g.FillRegion(r.Color, r.Region));
                        }
                    }
                }
                finally
                {
                    brushes.Dispose();
                }
            }
        }
    }
}