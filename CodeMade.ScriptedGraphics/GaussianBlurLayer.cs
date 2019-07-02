using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class GaussianBlurLayer : Layer
    {
        public float BlurRadius { get; set; }

        public GaussianBlurLayer()
        {
        }

        public GaussianBlurLayer(float blurRadius)
        {
            BlurRadius = blurRadius;
        }

        public override void Render(Graphics globalGraphics)
        {
            var img = new Bitmap((int)globalGraphics.VisibleClipBounds.Width, (int)globalGraphics.VisibleClipBounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (var g = Graphics.FromImage(img))
            {
                RenderShapes(g);
            }
            SuperfastBlur.GaussianBlur blur = new SuperfastBlur.GaussianBlur(img);
            globalGraphics.DrawImage(blur.Process((int)BlurRadius), new PointF(0, 0));
        }
    }
}
