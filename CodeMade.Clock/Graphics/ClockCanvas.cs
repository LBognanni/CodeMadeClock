using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

            var layerGroups = GroupLayers(skipLayers);

            foreach (var (isSkipLayerGroup, layerList) in layerGroups)
            {
                if (isSkipLayerGroup)
                {
                    newCanvas.Layers.AddRange(layerList);
                }
                else
                {
                    newCanvas.Layers.Add(MergeLayers(scaleFactor, newCanvas, layerList));
                }
            }

            return new ClockCanvas(_timer, newCanvas);
        }

        private Layer MergeLayers(float scaleFactor, Canvas newCanvas, List<Layer> layerList)
        {
            var bmp = new Bitmap((int)(_canvas.Width * scaleFactor), (int)(_canvas.Height * scaleFactor));
            using (var g = Graphics.FromImage(bmp))
            {
                foreach (var layer in layerList)
                {
                    layer.Render(g, scaleFactor);
                }
            }

            var wrap = new Layer();
            wrap.Shapes.Add(new BitmapShape(null)
            {
                FixedImage = bmp,
                Left = 0,
                Top = 0,
                Width = newCanvas.Width,
                Height = newCanvas.Height
            });
            return wrap;
        }


        private List<(bool, List<Layer>)> GroupLayers(Type[] skipLayers)
        {
            var layerGroups = new List<(bool, List<Layer>)>();
            foreach (var layer in _canvas.Layers)
            {
                var isSkipLayer = skipLayers.Contains(layer.GetType());
                if (!layerGroups.Any() || (isSkipLayer != layerGroups.Last().Item1))
                {
                    layerGroups.Add((isSkipLayer, new List<Layer>()));
                }
                layerGroups.Last().Item2.Add(layer);
            }

            return layerGroups;
        }
    }
}
