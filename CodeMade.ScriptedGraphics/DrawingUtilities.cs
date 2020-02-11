using System;
using System.Drawing;
using System.IO;

namespace CodeMade.ScriptedGraphics
{
    internal class DrawingUtilities
    {
        internal static Image LoadImage(string path)
        {
            using (var ms = new MemoryStream(File.ReadAllBytes(path)))
            {
                return Image.FromStream(ms);
            }
        }
    }
}