using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics
{
    public class DeleteCircleShape : CircleShape
    {
        protected override void RenderCircle(Graphics g, float diameter, float left, float top)
        {
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(left, top, diameter, diameter);
                using (var region = new Region(path))
                {
                    g.SetClip(region, CombineMode.Replace);
                    g.Clear(System.Drawing.Color.Transparent);
                    g.ResetClip();
                }
            }
        }
    }
}
