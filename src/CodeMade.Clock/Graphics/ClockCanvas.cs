using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock
{
    public class ClockCanvas : Canvas
    {
        protected readonly ITimer _timer;
        protected readonly Canvas _canvas;
        private readonly List<Layer> _layers;

        public ClockCanvas(ITimer timer, Canvas canvas) : base(canvas.Width, canvas.Height, "")
        {
            _timer = timer;
            _canvas = canvas;
            _layers = FindLayers(canvas.Layers).ToList();
            _hasSmoothSeconds = new Lazy<bool>(() => _layers.OfType<SecondsLayer>().Any(x => x.Smooth));
        }

        private IEnumerable<Layer> FindLayers(IEnumerable<Layer> layers)
        {
            foreach(var layer in layers)
            {
                foreach (var subLayer in FindLayers(layer.Shapes.Where(x => x.GetType().IsAssignableTo(typeof(Layer))).Cast<Layer>()))
                {
                    yield return subLayer;
                }
                yield return layer;
            }
        }

        private Lazy<bool> _hasSmoothSeconds;

        public bool HasSmoothSeconds => _hasSmoothSeconds.Value;

        public void Update()
        {
            var time = _timer.GetTime();
            foreach (var layer in _layers)
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
            var grouper = new LayerSlicer(
                typeof(HoursLayer),
                typeof(MinutesLayer),
                typeof(SecondsLayer)
            );

            var newCanvas = new Canvas(_canvas.Width, _canvas.Height, "#0000");

            var layerGroups = grouper.GroupLayers(_canvas.Layers).ToList();

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

        private Layer MergeLayers(float scaleFactor, Canvas newCanvas, IEnumerable<Layer> layerList)
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
    }
}
