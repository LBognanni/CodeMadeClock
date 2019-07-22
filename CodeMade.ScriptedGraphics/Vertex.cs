using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class Vertex
    {
        public Vertex()
        {
        }

        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        internal PointF AsPointF(float scaleFactor)
        {
            return new PointF(X * scaleFactor, Y * scaleFactor);
        }
    }
}