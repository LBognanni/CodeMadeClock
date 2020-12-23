using NUnit.Framework;
using System;
using System.Drawing;
using System.IO;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BitmapTesterBase
    {
        public enum PixelCompareMode
        {
            FullColor,
            RGB,
            RGBSimilar,
            RGBASimilar
        }

        public static void AssertBitmapsAreEqual(Bitmap expected, Bitmap actual, PixelCompareMode compareMode = PixelCompareMode.FullColor)
        {
            expected.SetResolution(actual.HorizontalResolution, actual.VerticalResolution);

            if (expected.Width != actual.Width)
                Assert.Fail($"Expected width {expected.Width}, actual {actual.Width}");
            if (expected.Height != actual.Height)
                Assert.Fail($"Expected height {expected.Height}, actual {actual.Height}");

            int differentPixels = 0;
            int maxDifferentPixels = (expected.Width * expected.Height) / 100;
            for (int x = 0; x < expected.Width; ++x)
            {
                for (int y = 0; y < expected.Height; ++y)
                {
                    var expectedPixel = expected.GetPixel(x, y);
                    var actualPixel = actual.GetPixel(x, y);
                    if (ArePixelsDifferent(expectedPixel, actualPixel, compareMode))
                    {
                        if (compareMode == PixelCompareMode.RGBASimilar || compareMode == PixelCompareMode.RGBSimilar)
                        {
                            differentPixels++;
                            if (differentPixels <= maxDifferentPixels)
                                continue;
                        }

                        DisplayDiagnosticImage(expected, actual);
                        Assert.Fail($"Expected pixel at ({x},{y}) to be {expectedPixel.ToHtml()} but was {actualPixel.ToHtml()}");
                        return;
                    }
                }
            }
        }

        private static bool ArePixelsDifferent(Color expectedPixel, Color actualPixel, PixelCompareMode compareMode)
        {
            switch (compareMode)
            {
                case PixelCompareMode.FullColor:
                    return (expectedPixel != actualPixel);
                case PixelCompareMode.RGB:
                    return (expectedPixel.R, expectedPixel.G, expectedPixel.B) != (actualPixel.R, actualPixel.G, actualPixel.B);
                case PixelCompareMode.RGBSimilar:
                    return Math.Abs(expectedPixel.R - actualPixel.R) + Math.Abs(expectedPixel.G - actualPixel.G) + Math.Abs(expectedPixel.B - actualPixel.B) > 8;
                case PixelCompareMode.RGBASimilar:
                    return Math.Abs(expectedPixel.R - actualPixel.R) + Math.Abs(expectedPixel.G - actualPixel.G) + Math.Abs(expectedPixel.B - actualPixel.B) > 8;
                default:
                    return true;
            }
        }

        private static void DisplayDiagnosticImage(Bitmap expected, Bitmap actual)
        {
            string filename = Path.GetTempFileName();
            using (Image bmp = new Bitmap(expected.Width * 2 + 5, expected.Height + 2))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(expected, 1, 1);
                    g.DrawImage(actual, expected.Width + 3, 1);
                }
                bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
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
            return DrawingUtilities.LoadImage(TestPath(fileName)) as Bitmap;
        }

        protected static string TestPath(string fileName)
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);
        }
    }
}
