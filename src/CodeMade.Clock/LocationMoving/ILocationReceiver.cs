using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.Clock.LocationMoving
{
    public interface ILocationReceiver
    {
        public Size Size { get; }
        public IEnumerable<Rectangle> Screens { get; }
    }
}
