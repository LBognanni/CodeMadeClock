using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics
{
    public class CircleShape : IShape
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public string Color { get; set; }

        public virtual void Render(Graphics g)
        {
            using (var brush = new SolidBrush(Color.ToColor()))
            {
                g.FillEllipse(brush, X - Radius, Y - Radius, X + Radius, Y + Radius);
            }
        }
    }
}
