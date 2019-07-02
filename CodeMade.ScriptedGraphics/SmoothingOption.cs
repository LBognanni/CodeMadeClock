using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    public class SmoothingOption : IDisposable
    {
        private readonly Graphics _graphics;
        private readonly SmoothingMode _previousSmoothingMode;

        public SmoothingOption(Graphics g, SmoothingMode mode)
        {
            _graphics = g;
            _previousSmoothingMode = g.SmoothingMode;
            g.SmoothingMode = mode;
        }

        public void Dispose()
        {
            _graphics.SmoothingMode = _previousSmoothingMode;
        }
    }
}
