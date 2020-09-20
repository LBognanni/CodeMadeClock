using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    [TestFixture]
    class BitmapShapeTests : BitmapTesterBase
    {
        [TestCase(@"testimages\colors.png", 231, 229)]
        [TestCase(@"testimages\default_Test.png", 273, 273)]
        public void TestImage(string path, int width, int heigth)
        {
            var reference = LoadLocalBitmap(path);
            var canvas = new Canvas(width, heigth, "red");
            canvas.Add(new BitmapShape(new FileReader(TestPath("testimages")))
            {
                Left = 0,
                Top = 0,
                Width = width,
                Height = heigth,
                Path = TestPath(path)
            });
            AssertBitmapsAreEqual(reference, canvas.Render());

        }
    }
}
