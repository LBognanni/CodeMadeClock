using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    class SimpleBitmapTests : BitmapTesterBase
    {
        [Test]
        public void When_Comparing_the_same_image_it_should_be_equal()
        {
            var bmp1 = LoadLocalBitmap(@"testimages\colors.png");
            var bmp2 = LoadLocalBitmap(@"testimages\colors.png");
            AssertBitmapsAreEqual(bmp1, bmp2);
        }

        [Test]
        public void When_Comparing_Different_Size_images_it_should_be_False()
        {
            var bmp1 = LoadLocalBitmap(@"testimages\colors.png");
            var bmp2 = LoadLocalBitmap(@"testimages\random.png");
            AssertBitmapsAreNotEqual(bmp1, bmp2);
        }

        [Test]
        public void When_Comparing_Different_images_of_same_size_it_should_be_False()
        {
            var bmp1 = LoadLocalBitmap(@"testimages\colors.png");
            var bmp2 = LoadLocalBitmap(@"testimages\colors_inv.png");
            AssertBitmapsAreNotEqual(bmp1, bmp2);
        }
    }
}
