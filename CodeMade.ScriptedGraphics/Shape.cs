using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    public class Shape : IShape
    {
        [XmlIgnore]
        public IEnumerable<Vertex> Vertices { get; set; }
        public string Color { get; set; }
        public string Path
        {
            get
            {
                return String.Join(" ", Vertices.Select(v => $"{v.X},{v.Y}"));
            }
            set
            {
                Vertices = VertexArrayFromString(value);
            }
        }


        public static IEnumerable<Vertex> VertexArrayFromString(string path)
        {
            const string dividers = ", ";
            var numbers = path.Split(dividers.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            Vertex v = new Vertex();
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    v = new Vertex() { X = float.Parse(numbers[i]) };
                }
                else
                {
                    v.Y = float.Parse(numbers[i]);
                    yield return v;
                }
            }
        }

        public void Render(Graphics g, float scaleFactor = 1)
        {
            var points = GetPoints(scaleFactor);
            RectangleF rect = GetBounds(points);
            using (var brush = Color.ParseBrush(rect))
            {
                g.FillPath(brush, new GraphicsPath(points, GetPointTypes(), FillMode.Alternate));
            }
        }

        private RectangleF GetBounds(PointF[] points)
        {
            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
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