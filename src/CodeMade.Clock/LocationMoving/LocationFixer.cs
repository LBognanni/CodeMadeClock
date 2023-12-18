using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock.LocationMoving
{
    public class LocationFixer : ILocationFixer
    {
        private readonly IScreens _target;

        public LocationFixer(IScreens target)
        {
            _target = target;
        }

        public Point FixLocation(Point location, Size size)
        {
            var clockRect = new Rectangle(location, size);
            var clockPt = FindCenter(clockRect);
            var intersects = new List<Rectangle>();

            foreach (var screen in _target.Screens)
            {
                if (screen.Contains(clockRect))
                {
                    return location;
                }
                else if (screen.IntersectsWith(clockRect))
                {
                    intersects.Add(screen);
                }
            }

            return intersects.Count switch
            {
                0 => MoveToClosestScreen(location, clockPt, size),
                1 => MoveToScreen(location, intersects[0], size),
                _ => location
            };

        }

        private Point MoveToClosestScreen(Point location, Point clockPt, Size size)
        {
            var screens = _target.Screens.Select(s => (Screen: s, Distance: FindDistanceSquared(s, clockPt)));
            return MoveToScreen(location, screens.OrderBy(s => s.Distance).First().Screen, size);
        }

        private Point MoveToScreen(Point location, Rectangle screen, Size size)
        {
            if (location.X < screen.Left)
            {
                location.X = screen.Left;
            }
            else if (location.X + size.Width > screen.Right)
            {
                location.X = screen.Right - size.Width;
            }

            if (location.Y < screen.Top)
            {
                location.Y = screen.Top;
            }
            else if (location.Y + size.Height > screen.Bottom)
            {
                location.Y = screen.Bottom - size.Height;
            }

            return location;
        }

        private Point FindCenter(Rectangle bounds)
        {
            return new Point(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
        }

        private decimal FindDistanceSquared(Rectangle bounds, Point pt)
        {
            var pt2 = FindCenter(bounds);
            return (pt.X - pt2.X) * (pt.X - pt2.X) + (pt.Y - pt2.Y) * (pt.Y - pt2.Y);
        }
    }
}
