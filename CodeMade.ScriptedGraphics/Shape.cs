using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    public class Shape : IShape
    {

        public IEnumerable<Vertex> Vertices { get; set; }
        public string Color { get; set; }

        public void Render(Graphics g)
        {
            using (var brush = new SolidBrush(Color.ToColor()))
                g.FillPath(brush, new System.Drawing.Drawing2D.GraphicsPath(GetPoints(), GetPointTypes(), System.Drawing.Drawing2D.FillMode.Alternate)); ;
        }

        private byte[] GetPointTypes()
        {
            return Vertices.Select(v => (byte)PathPointType.Line).ToArray();
        }

        private PointF[] GetPoints()
        {
            return Vertices.Select(v => new PointF(v.X, v.Y)).ToArray();
        }
    }
}