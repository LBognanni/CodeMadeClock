using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A gaussian blur layer. Anything contained in this layer will be blurred
    /// </summary>
    /// <inheritdoc cref="Layer"/>
    /// <example>
	///	{	
	///	    "$type": "GaussianBlurLayer",
    ///     "BlurRadius": 0.85,
    ///     "Shapes": [
    ///      // ... shapes ...
    ///     ]
	///	},
    /// </example>
    public class GaussianBlurLayer : Layer
    {
        /// <summary>
        /// The blur radius
        /// </summary>
        public float BlurRadius { get; set; }

        public GaussianBlurLayer()
        {
        }

        public override Layer Copy()
            => new GaussianBlurLayer
            {
                Offset = Offset,
                Rotate = Rotate,
                BlurRadius = BlurRadius
            };

        public GaussianBlurLayer(float blurRadius)
        {
            BlurRadius = blurRadius;
        }

        public override void Render(Graphics globalGraphics, float scaleFactor)
        {
            var img = new Bitmap((int)globalGraphics.VisibleClipBounds.Width, (int)globalGraphics.VisibleClipBounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (var g = Graphics.FromImage(img))
            {
                base.Render(g, scaleFactor);
            }
            SuperfastBlur.GaussianBlur blur = new SuperfastBlur.GaussianBlur(img);
            globalGraphics.DrawImage(blur.Process((int)(BlurRadius * scaleFactor)), new PointF(0, 0));
        }
    }
}
