using NUnit.Framework;
using System;
using System.Drawing;
using System.IO;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BitmapTesterBase
    {
        public static void AssertBitmapsAreEqual(Bitmap expected, Bitmap actual)
        {
            if (expected.Width != actual.Width)
                Assert.Fail($"Expected width {expected.Width}, actual {actual.Width}");
            if (expected.Height != actual.Height)
                Assert.Fail($"Expected height {expected.Height}, actual {actual.Height}");

            for (int x = 0; x < expected.Width; ++x)
            {
                for (int y = 0; y < expected.Height; ++y)
                {
                    var expectedPixel = expected.GetPixel(x, y);
                    var actualPixel = actual.GetPixel(x, y);
                    if (expectedPixel != actualPixel)
                    {
                        string filename = System.IO.Path.GetTempFileName();
                        actual.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                        System.Diagnostics.Process.Start("mspaint.exe", filename);
                        Assert.Fail($"Expected pixel at ({x},{y}) to be {expectedPixel.ToHtml()} but was {actualPixel.ToHtml()}");
                        return;
                    }
                }
            }
        }
        public static void AssertBitmapsAreNotEqual(Bitmap expected, Bitmap actual)
        {
            if (expected.Width != actual.Width)
                return;
            if (expected.Height != actual.Height)
                return;

            for (int x = 0; x < expected.Width; ++x)
            {
                for (int y = 0; y < expected.Height; ++y)
                {
                    var expectedPixel = expected.GetPixel(x, y);
                    var actualPixel = actual.GetPixel(x, y);
                    if (expectedPixel != actualPixel)
                    {
                        return;
                    }
                }
            }

            Assert.Fail("Bitmaps are equal");
        }

        protected Bitmap LoadLocalBitmap(string fileName)
        {
            return Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", fileName)) as Bitmap;
        }
    }
}
