using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class Shape : IShape
    {
        public IEnumerable<Vertex> Vertices { get; set; }
        public string Color { get; set; }

        public void Render(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}