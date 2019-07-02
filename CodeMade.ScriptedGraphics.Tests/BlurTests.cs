using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BlurTests : BitmapTesterBase
    {
        [Test]
        public void BlurRectangleLayer()
        {
            Canvas canvas = new Canvas(100, 100, "#fff");
            canvas.Layers.Add(new GaussianBlurLayer(5));
            canvas.Add(new RectangleShape(20, 20, 60, 60, "#000"));
            var img = canvas.Render();
            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\rectblur.png"), img);
        }

        [Test]
        public void BlurLayer_ShouldOnlyBlurOneLayer()
        {
            Canvas canvas = new Canvas(100, 100, "#fff");
            canvas.Add(new RectangleShape(0, 0, 50, 50, "red"));
            canvas.Layers.Add(new GaussianBlurLayer(5));
            canvas.Add(new RectangleShape(20, 20, 60, 60, "#000"));
            canvas.Layers.Add(new Layer());
            canvas.Add(new RectangleShape(50, 50, 50, 50, "blue"));

            var img = canvas.Render();
            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\rectblur_layers.png"), img);
        }

    }
}
