using System.Drawing;

namespace CodeMade.Clock.LocationMoving
{
    public interface ILocationFixer
    {
        Point FixLocation(Point location);
    }
}