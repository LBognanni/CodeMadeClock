using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    [TestFixture]
    class BitmapShapeTests : BitmapTesterBase
    {
        [Test]
        public void TestImage()
        {
            var reference = LoadLocalBitmap(@"testimages\colors.png");
            var canvas = new Canvas(231, 229, "red");
            canvas.Add(new BitmapShape(new PathResolver(TestPath("testimages")))
            {
                Left = 0,
                Top = 0,
                Width = 231,
                Height = 229,
                Path = TestPath(@"testimages\colors.png")
            });
            AssertBitmapsAreEqual(reference, canvas.Render());

        }
    }
}
