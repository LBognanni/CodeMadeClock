using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A generic shape composed of a series of points
    /// </summary>
    /// <example>
    /// {
    ///     "$type": "Shape",
    ///     "Color": "#8bf",
    ///     "Path": "-1.75,6.5 -1.75,-20.5 1.75,-20.5 1.75,6.5"
    /// }
    /// </example>
    public class Shape : IShape
    {
        [XmlIgnore]
        public IEnumerable<Vertex> Vertices { get; set; }

        /// <summary>
        /// Fill color or gradient
        /// </summary>
        /// <see cref="Colors"/>
        public string Color { get; set; }

        /// <summary>
        /// A string value containing all the x and y positions for each point separated by space or comma.
        /// Example: `"0,1 1,1 1,0 0,0"` will be a rectangle 1 point in size
        /// </summary>
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
                var state = g.BeginContainer();

                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.FillPath(brush, new GraphicsPath(points, GetPointTypes(), FillMode.Alternate));

                g.EndContainer(state);
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