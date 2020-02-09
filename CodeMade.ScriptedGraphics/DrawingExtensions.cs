using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics
{
    public static class DrawingExtensions
    {
        public static PointF Minus(this PointF p, float x, float y)
        {
            return new PointF(p.X - x, p.Y - y);
        }
    }
}
