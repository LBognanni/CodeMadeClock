using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock.LocationMoving
{
    public class LocationFixer : ILocationFixer
    {
        private readonly ILocationReceiver _target;

        public LocationFixer(ILocationReceiver target)
        {
            _target = target;
        }

        public Point FixLocation(Point location)
        {
            var clockRect = new Rectangle(location, _target.Size);
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
                0 => MoveToClosestScreen(location, clockPt),
                1 => MoveToScreen(location, intersects[0]),
                _ => location
            };

        }

        private Point MoveToClosestScreen(Point location, Point clockPt)
        {
            var screens = _target.Screens.Select(s => (Screen: s, Distance: FindDistanceSquared(s, clockPt)));
            return MoveToScreen(location, screens.OrderBy(s => s.Distance).First().Screen);
        }

        private Point MoveToScreen(Point location, Rectangle screen)
        {
            if (location.X < screen.Left)
            {
                location.X = screen.Left;
            }
            else if (location.X + _target.Size.Width > screen.Right)
            {
                location.X = screen.Right - _target.Size.Width;
            }

            if (location.Y < screen.Top)
            {
                location.Y = screen.Top;
            }
            else if (location.Y + _target.Size.Height > screen.Bottom)
            {
                location.Y = screen.Bottom - _target.Size.Height;
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
