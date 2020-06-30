using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CodeMade.ScriptedGraphics
{
    public class CachedLayer : Layer
    {
        public override void Render(Graphics graphics, float scaleFactor = 1)
        {
            using (var bmp = new Bitmap((int)graphics.VisibleClipBounds.Width, (int)graphics.VisibleClipBounds.Height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Transform = graphics.Transform;
                g.CompositingMode = graphics.CompositingMode;
                g.CompositingQuality = graphics.CompositingQuality;
                g.PixelOffsetMode = graphics.PixelOffsetMode;

                base.Render(g, scaleFactor);

                graphics.DrawImageUnscaled(bmp, 0, 0);
            }
        }
    }
}
