using System;
using System.Drawing;
using CodeMade.ScriptedGraphics;

namespace CodeMade.Clock
{
    internal class ClockCanvas : Canvas
    {
        protected ITimer _timer;
        protected Canvas _canvas;

        public ClockCanvas(ITimer timer, Canvas canvas) : base(canvas.Width, canvas.Height, "")
        {
            _timer = timer;
            _canvas = canvas;
            _hasSmoothSeconds = new Lazy<bool>(() =>
            {
                foreach (var layer in _canvas.Layers)
                {
                    if (layer is SecondsLayer secondsLayer)
                    {
                        if (secondsLayer.Smooth)
                            return true;
                    }
                }
                return false;
            });
        }

        private Lazy<bool> _hasSmoothSeconds;

        public bool HasSmoothSeconds => _hasSmoothSeconds.Value;

        public void Update()
        {
            var time = _timer.GetTime();
            foreach (var layer in _canvas.Layers)
            {
                LayerExtensions.UpdateLayer(layer, time);
            }
        }

        public override Bitmap Render(float scaleFactor = 1)
        {
            return _canvas.Render(scaleFactor);
        }
    }
}
