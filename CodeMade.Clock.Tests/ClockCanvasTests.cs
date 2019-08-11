using CodeMade.ScriptedGraphics;
using CodeMade.ScriptedGraphics.Tests;
using NUnit.Framework;
using System;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class ClockCanvasTests : BitmapTesterBase
    {
        class TestTimer : ITimer
        {
            private DateTime fakeDate;

            public TestTimer(DateTime fixedDate)
            {
                fakeDate = fixedDate;
            }

            public DateTime GetTime()
            {
                return fakeDate;
            }
        }

        [Test]
        public void OptimizedCanvas_Renders_Like_Canvas()
        {
            var canvas = new Canvas(100, 100, "");
            canvas.Add(new RectangleShape(0, 0, 20, 20, "white"));
            canvas.Add(new CircleShape(10, 10, 10, "red"));

            canvas.Add(new RectangleShape(80, 80, 20, 20, "white"));
            canvas.Add(new CircleShape(90, 90, 10, "red"));

            canvas.Layers.Add(new SecondsLayer() { Offset = new Vertex(50, 50) });
            canvas.Add(new RectangleShape(-1, -30, 2, 32, "blue"));
            canvas.Layers.Add(new Layer());

            canvas.Add(new RectangleShape(0, 80, 20, 20, "white"));
            canvas.Add(new CircleShape(10, 90, 10, "green"));

            canvas.Add(new RectangleShape(80, 0, 20, 20, "white"));
            canvas.Add(new CircleShape(90, 10, 10, "green"));

            var clockCanvas = new ClockCanvas(new TestTimer(DateTime.Today.AddHours(3).AddMinutes(30).AddSeconds(45)), canvas);
            var optimizedCanvas = clockCanvas.OptimizeFor(1);
            clockCanvas.Update();
            optimizedCanvas.Update();
            var bmp = optimizedCanvas.Render();
            AssertBitmapsAreEqual(clockCanvas.Render(), bmp);
        }
    }
}
