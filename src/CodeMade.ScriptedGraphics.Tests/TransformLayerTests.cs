using NUnit.Framework;
using System.Linq;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class TransformLayerTests : BitmapTesterBase
    {
        [Test]
        public void Rotate45Degrees_rect()
        {
            Canvas canvas = new Canvas(100, 100, "white");
            canvas.Add(new RectangleShape(-25, -25, 50, 50, "#000"));
            canvas.Layers.Last().Rotate = 45;
            canvas.Layers.Last().Offset = new Vertex(50, 50);

            canvas.Layers.Add(new Layer());
            canvas.Add(new RectangleShape(25, 25, 50, 50, "red"));

            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\rotate45deg.png"), canvas.Render());
        }

        [Test]
        public void RepeatRotateLayerTest()
        {
            Canvas expected = new Canvas(100, 100, "white");
            expected.Layers.Add(new Layer { Rotate = 0, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer{ Rotate = 30, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer { Rotate = 60, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer { Rotate = 90, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer { Rotate = 120, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer { Rotate = 150, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));
            expected.Layers.Add(new Layer { Rotate = 180, Offset = new Vertex(50, 50) });
            expected.Add(new CircleShape(0, -20, 5, "red"));

            Canvas actual = new Canvas(100, 100, "white");
            actual.Layers.Add(new RotateRepeatLayer { RepeatCount = 7, RepeatRotate = 30, Offset = new Vertex(50, 50) });
            actual.Add(new CircleShape(0, -20, 5, "red"));

            AssertBitmapsAreEqual(expected.Render(), actual.Render());
        }
    }
}
