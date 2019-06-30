using NUnit.Framework;
using System.Drawing;
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
            canvas.Layers.Last().TransformRotate = 45;
            canvas.Layers.Last().Offset = new Vertex(50, 50);

            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\rotate45deg.png"), (Bitmap)canvas.Render());
        }
    }
}
