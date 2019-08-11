using System;
using System.Drawing;
using System.Linq;
using CodeMade.ScriptedGraphics;

namespace CodeMade.Clock
{
    public class ClockCanvas : Canvas
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

        public ClockCanvas OptimizeFor(float scaleFactor)
        {
            Type[] skipLayers =
            {
                typeof(HoursLayer),
                typeof(MinutesLayer),
                typeof(SecondsLayer)
            };
            var newCanvas = new Canvas(_canvas.Width, _canvas.Height, "#0000");

            Bitmap bmp = null;
            Graphics g = null;

            foreach(var layer in _canvas.Layers)
            {
                if (skipLayers.Contains(layer.GetType()))
                {
                    AddBitmpaToCanvas(newCanvas, ref bmp, ref g);

                    newCanvas.Add(layer);
                    continue;
                }

                if (bmp == null)
                {
                    bmp = new Bitmap((int)(_canvas.Width * scaleFactor), (int)(_canvas.Height * scaleFactor));
                    g = Graphics.FromImage(bmp);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                }
                layer.Render(g, scaleFactor);
            }
            AddBitmpaToCanvas(newCanvas, ref bmp, ref g);

            return new ClockCanvas(this._timer, newCanvas);
        }

        private static void AddBitmpaToCanvas(Canvas newCanvas, ref Bitmap bmp, ref Graphics g)
        {
            if (bmp != null)
            {
                newCanvas.Layers.Add(new Layer());
                newCanvas.Add(new BitmapShape
                {
                    Image = bmp,
                    Left = 0,
                    Top = 0,
                    Width = newCanvas.Width,
                    Height = newCanvas.Height
                });
                g.Dispose();
                g = null;
                bmp = null;
            }
        }
    }
}
