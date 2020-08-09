using CodeMade.ScriptedGraphics;
using CodeMade.ScriptedGraphics.Tests;
using NodaTime;
using NUnit.Framework;
using System;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class ClockCanvasTests : BitmapTesterBase
    {
        class TestTimer : ITimer
        {
            private Instant fakeDate;

            public TestTimer(Instant fixedDate)
            {
                fakeDate = fixedDate;
            }

            public Instant GetTime()
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

            var clockCanvas = new ClockCanvas(new TestTimer(Instant.FromDateTimeOffset(DateTime.Today.AddHours(3).AddMinutes(30).AddSeconds(45))), canvas);
            var optimizedCanvas = clockCanvas.OptimizeFor(1);
            clockCanvas.Update();
            optimizedCanvas.Update();
            var bmp = optimizedCanvas.Render();
            AssertBitmapsAreEqual(clockCanvas.Render(), bmp);
        }

        [Test]
        public void OptimizedCanvas_Renders_Like_Canvas_When_Bitmap()
        {
            var canvas = new Canvas(273, 273, "");
            canvas.Add(new RectangleShape(0, 0, 20, 20, "white"));
            canvas.Add(new BitmapShape(new PathResolver(TestPath("")))
            {
                Left = 0,
                Top = 0,
                Width = 273,
                Height = 273,
                Path = TestPath(@"default_Test.png")
            });

            var clockCanvas = new ClockCanvas(new TestTimer(Instant.FromDateTimeOffset(DateTime.Today.AddHours(3).AddMinutes(30).AddSeconds(45))), canvas);
            var optimizedCanvas = clockCanvas.OptimizeFor(1);
            clockCanvas.Update();
            optimizedCanvas.Update();
            var bmp = optimizedCanvas.Render();
            AssertBitmapsAreEqual(clockCanvas.Render(), bmp);
        }


        [Test]
        public void OptimizedCanvas_Renders_Like_Canvas_When_Holes()
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
            
            canvas.Layers.Add(new CachedLayer());
            canvas.Add(new CircleShape(50, 50, 50, "red"));
            canvas.Add(new DeleteCircleShape { Position = new Vertex(50, 50), Radius = 45 });

            var clockCanvas = new ClockCanvas(new TestTimer(Instant.FromDateTimeOffset(DateTime.Today.AddHours(3).AddMinutes(30).AddSeconds(45))), canvas);
            var optimizedCanvas = clockCanvas.OptimizeFor(1);
            clockCanvas.Update();
            optimizedCanvas.Update();
            var bmp = optimizedCanvas.Render();
            AssertBitmapsAreEqual(clockCanvas.Render(), bmp, PixelCompareMode.RGBASimilar);
        }
    }
}
