using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    class CustomFontTextShape : TextShape
    {
        public CustomFontTextShape(IFileReader fileReader) : base(fileReader)
        {
        }

        public CustomFontTextShape(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color) : base(fileReader, text, fontName, fontSizePx, position, color)
        {
        }
    }
}
