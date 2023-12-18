using CodeMade.Clock.LocationMoving;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class LocationFixerTest
    {
        [Test]
        public void WhenSettingLocationInsideScreen_DoesNothing()
        {
            var receiver = new TestLocationReceiver
            {
                PrimaryScreen = new Rectangle(0, 0, 1000, 1000),
                Screens = new[] { new Rectangle(0, 0, 1000, 1000) }
            };
            var setter = new LocationFixer(receiver);
            var targetLocation = new Point(50, 50);
            Assert.AreEqual(targetLocation, setter.FixLocation(targetLocation, receiver.Size));
        }

        [Test]
        public void WhenSettingLocationOnMultipleScreens_CanIntersectAdjacentScreens()
        {
            var receiver = new TestLocationReceiver
            {
                Screens = new[] { new Rectangle(0, 0, 1000, 1000), new Rectangle(1000, 0, 1000, 1000) }
            };
            var setter = new LocationFixer(receiver);
            var targetLocation = new Point(950, 50);
            Assert.AreEqual(targetLocation, setter.FixLocation(targetLocation, receiver.Size));
        }

        private static IEnumerable<object[]> TestCasesForClosestScreen()
        {
            yield return new object[] { new Point(550, 80), new Point(400, 80), new[] { new Rectangle(0, 0, 500, 500) } };
            yield return new object[] { new Point(-550, 80), new Point(0, 80), new[] { new Rectangle(0, 0, 500, 500) } };

            yield return new object[] { new Point(80, 550), new Point(80, 400), new[] { new Rectangle(0, 0, 500, 500) } };
            yield return new object[] { new Point(80, -550), new Point(80, 0), new[] { new Rectangle(0, 0, 500, 500) } };

            yield return new object[] { new Point(550, 80), new Point(550, 80), new[] {
                new Rectangle(0, 0, 500, 500),
                new Rectangle(500, 0, 500, 500),
            } };

            yield return new object[] { new Point(550, -980), new Point(550, 0), new[] {
                new Rectangle(0, 0, 500, 500),
                new Rectangle(500, 0, 500, 500),
                new Rectangle(1000, 0, 500, 500),
                new Rectangle(1500, 0, 500, 500),
            } };

        }

        [TestCaseSource(nameof(TestCasesForClosestScreen))]
        public void WhenOutsideOfBounds_MovesToTheClosestScreen(Point location, Point expected, Rectangle []screens)
        {
            var receiver = new TestLocationReceiver
            {
                Screens = screens
            };
            var setter = new LocationFixer(receiver);
            Assert.AreEqual(expected, setter.FixLocation(location, receiver.Size));
        }

        class TestLocationReceiver : IScreens
        {
            public Point Location { get; set; }
            public Size Size => new Size(100, 100);

            public Rectangle PrimaryScreen { get; set; }

            public IEnumerable<Rectangle> Screens { get; set; }
        }
    }
}
