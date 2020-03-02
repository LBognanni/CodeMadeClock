using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.LocationMoving
{
    public interface ILocationReceiver
    {
        public Point Location { get; set; }
        public Size Size { get; }
        public IEnumerable<Rectangle> Screens { get; }
    }
}
