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

        public void Render(Graphics g, float scaleFactor = 1)
        {
            using (var brush = new SolidBrush(Color.ToColor()))
                g.FillPath(brush, new System.Drawing.Drawing2D.GraphicsPath(GetPoints(scaleFactor), GetPointTypes(), System.Drawing.Drawing2D.FillMode.Alternate)); ;
        }

        private byte[] GetPointTypes()
        {
            return Vertices.Select(v => (byte)PathPointType.Line).ToArray();
        }

        private PointF[] GetPoints(float scaleFactor)
        {
            return Vertices.Select(v => new PointF(v.X * scaleFactor, v.Y * scaleFactor)).ToArray();
        }
    }
}