using System;
using System.Drawing;
using System.IO;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BitmapTesterBase
    {
        public static bool AreBitmapsEqual(Bitmap expected, Bitmap actual)
        {
            if (expected.Width != actual.Width)
                return false;
            if (expected.Height != actual.Height)
                return false;

            for (int x = 0; x < expected.Width; ++x)
            {
                for (int y = 0; y < expected.Height; ++y)
                {
                    if (expected.GetPixel(x, y) != actual.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected Bitmap LoadLocalBitmap(string fileName)
        {
            return Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", fileName)) as Bitmap;
        }
    }
}
