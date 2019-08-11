using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

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
                        DisplayDiagnosticImage(expected, actual);
                        Assert.Fail($"Expected pixel at ({x},{y}) to be {expectedPixel.ToHtml()} but was {actualPixel.ToHtml()}");
                        return;
                    }
                }
            }
        }

        private static void DisplayDiagnosticImage(Bitmap expected, Bitmap actual)
        {
            string filename = System.IO.Path.GetTempFileName();
            using (Image bmp = new Bitmap(expected.Width * 2, expected.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImageUnscaled(expected, 0, 0);
                    g.DrawImageUnscaled(actual, expected.Width + 1, 0);
                }
                bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            System.Diagnostics.Process.Start("mspaint.exe", filename);
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
            return Image.FromFile(TestPath(fileName)) as Bitmap;
        }

        protected static string TestPath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", fileName);
        }
    }
}
