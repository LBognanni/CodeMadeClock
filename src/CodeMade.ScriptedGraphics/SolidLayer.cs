using CodeMade.ScriptedGraphics.Colors;
using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A layer that is filled with a solid color
    /// This is the same as a normal Layer, but it is filled with one color instead of being transparent
    /// </summary>
    /// <inheritdoc cref="Layer"/>
    public class SolidLayer : Layer
    {
        /// <summary>
        /// A color name or string. Cannot be a gradient
        /// </summary>
        /// <see cref="Colors"/>
        public string BackgroundColor { get; set; }
        
        public SolidLayer(string backgroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        public override Layer Copy()
            => new SolidLayer(BackgroundColor)
            {
                Offset = Offset,
                Rotate = Rotate
            };

        protected override void BeforeTransform(Graphics g, float scaleFactor)
        {
            using (var smoothing = new SmoothingOption(g, System.Drawing.Drawing2D.SmoothingMode.Default))
            {
                using (var brush = new SolidBrush(BackgroundColor.ToColor()))
                {
                    g.FillRectangle(brush, g.VisibleClipBounds);
                }
            }
            base.BeforeTransform(g, scaleFactor);
        }
    }
}