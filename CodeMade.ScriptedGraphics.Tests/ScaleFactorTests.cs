using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    class ScaleFactorTests : BitmapTesterBase
    {
        [Test]
        public void when_drawing_a_circle_at_1x()
        {
            var shape = new CircleShape(99.5f, 99.5f, 99.5f, "#000");

            var canvas = new Canvas(200, 200, "#fff");
            canvas.Add(shape);

            var img = canvas.Render(1);
            AssertBitmapsAreEqual(LoadLocalBitmap("testimages/blackcircle.png"), img);
        }

        [Test]
        public void when_drawing_a_circle_at_2x()
        {
            var shape = new CircleShape(99.5f, 99.5f, 99.5f, "#000");

            var canvas = new Canvas(300, 200, "#fff");
            canvas.Add(shape);

            var img = canvas.Render(2);
            Assert.AreEqual(600, img.Width);
            Assert.AreEqual(400, img.Height);
            AssertBitmapsAreEqual(LoadLocalBitmap("testimages/circle600x400.png"), img);
        }

        [Test]
        public void when_drawing_a_shape_at_15x()
        {
            Canvas canvas = new Canvas(10, 10, "white");
            
            canvas.Add(new Shape
            {
                Color = "#000",
                Vertices = VertexArrayFromString("0,0 10,0, 10,10")
            });

            var img = canvas.Render(15);
            Assert.AreEqual(150, img.Width);
            Assert.AreEqual(150, img.Height);
            AssertBitmapsAreEqual(LoadLocalBitmap("testimages/triangle150x150.png"), img);
        }

        [Test]
        public void offset_Should_Scale()
        {
            Canvas canvas = new Canvas(20, 20, "white");
            canvas.Layers.Add(new Layer { Offset = new Vertex(10, 10) });
            canvas.Add(new RectangleShape(0, 0, 10, 10, "black"));

            Canvas reference = new Canvas(200, 200, "white");
            reference.Add(new RectangleShape(100, 100, 100, 100, "black"));

            AssertBitmapsAreEqual(reference.Render(), canvas.Render(10));
        }
    }
}
