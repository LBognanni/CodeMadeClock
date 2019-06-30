using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
