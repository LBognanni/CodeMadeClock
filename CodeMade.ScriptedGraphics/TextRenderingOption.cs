using System;
using System.Drawing;
using System.Drawing.Text;

namespace CodeMade.ScriptedGraphics
{
    internal class TextRenderingOption : IDisposable
    {
        private readonly Graphics _g;
        private TextRenderingHint _previousTextRenderingMode;

        public TextRenderingOption(Graphics g, TextRenderingHint newTextRenderingMode)
        {
            _g = g;
            _previousTextRenderingMode = g.TextRenderingHint;
            g.TextRenderingHint = newTextRenderingMode;
        }

        public void Dispose()
        {
            _g.TextRenderingHint = _previousTextRenderingMode;
        }
    }
}