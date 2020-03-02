using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.LocationMoving
{
    public class LocationSetter
    {
        private readonly ILocationReceiver _target;

        public LocationSetter(ILocationReceiver target)
        {
            _target = target;
        }

        public void SetLocation(Point location)
        {
            var clockRect = new Rectangle(location, _target.Size);
            var clockPt = FindCenter(clockRect);
            var intersects = new List<Rectangle>();

            foreach (var screen in _target.Screens)
            {
                if (screen.Contains(clockRect))
                {
                    _target.Location = location;
                    return;
                }
                else if (screen.IntersectsWith(clockRect))
                {
                    intersects.Add(screen);
                }
            }

            switch (intersects.Count)
            {
                case 0:
                    // Not on any screen - move in the closest one
                    var screens = _target.Screens.Select(s => (s, FindDistanceSquared(s, clockPt)));
                    MoveToScreen(location, screens.OrderBy(s => s.Item2).First().s);
                    break;
                case 1:
                    // Intersects one screen - move to it
                    MoveToScreen(location, intersects[0]);
                    break;
                default:
                    // Intersect multiple screens - this might be fine (in between two side-by-side screens)
                    _target.Location = location;
                    break;
            }
        }

        private void MoveToScreen(Point location, Rectangle screen)
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

            _target.Location = location;
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
