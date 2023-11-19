using CodeMade.ScriptedGraphics.Colors;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A donut shape: a circle shape with an inner circle blanked out
    /// </summary>
    /// <inheritdoc cref="CircleShape" />
    /// <example>
	/// {
	/// 	"$type": "DonutShape",
	/// 	"Position": {
	/// 		"X": 52,
	/// 		"Y": 52
	/// 	},
	/// 	"Radius": 49,
	/// 	"Color": "30-#bbb-#777",
	/// 	"InnerRadius": 46
	/// }
    /// </example>
    public class DonutShape : CircleShape
    {
        /// <summary>
        /// Inner radius of the donut. Must be smaller than the radius
        /// </summary>
        public float InnerRadius { get; set; }

        public DonutShape()
        {
        }

        public override void Render(Graphics g, float scaleFactor = 1)
        {
            using var path = new GraphicsPath();
            var rect = AddEllipse(path, Radius, scaleFactor);
            AddEllipse(path, InnerRadius, scaleFactor);

            var state = g.BeginContainer();

            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var thing in Color.ParseBrush(rect))
            {
                thing.Match(
                    brush =>
                    {
                        g.FillPath(brush, path);
                        brush.Dispose();
                    },
                    coloredRegion =>
                    {
                        coloredRegion.Region.Intersect(path);
                        g.FillRegion(coloredRegion.Color, coloredRegion.Region);
                        coloredRegion.Dispose();
                    }
                );
            }

            g.EndContainer(state);
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
