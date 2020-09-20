using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    class SerializationTests : BitmapTesterBase
    {
        Canvas GetCanvas()
        {
            var canvas = new Canvas(100, 100, "#fff");
            canvas.Add(new RectangleShape(0, 0, 50, 50, "red"));
            canvas.Layers.Add(new GaussianBlurLayer(5));
            canvas.Add(new RectangleShape(20, 20, 60, 60, "#000"));
            canvas.Layers.Add(new Layer());
            canvas.Add(new RectangleShape(50, 50, 50, 50, "blue"));
            canvas.Add(new TextShape("Testing 123", "Tahoma", 10, new Vertex(10, 50), "#080"));

            return canvas;
        }

        [Test]
        public void Canvas_Should_Serialize()
        {
            var canvas = GetCanvas();

            canvas.Save(TestPath(@"testimages\test.json"));

        }

        [Test]
        public void Canvas_Should_Deserialize()
        {
            var loaded = Canvas.Load(TestPath(@"testimages\test.json"));
            var reference = GetCanvas();
            AssertBitmapsAreEqual(reference.Render(), loaded.Render());
        }

    }
}
