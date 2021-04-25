using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A point
    /// </summary>
    public struct Vertex
    {
        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X Coordinate
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y Coordinate
        /// </summary>
        public float Y { get; set; }

        internal PointF AsPointF(float scaleFactor)
        {
            return new PointF(X * scaleFactor, Y * scaleFactor);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}