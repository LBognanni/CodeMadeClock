using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.Clock.LocationMoving
{
    public interface IScreens
    {
        public IEnumerable<Rectangle> Screens { get; }
    }
}
