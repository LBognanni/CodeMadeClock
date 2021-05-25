using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMade.Clock
{
    public class LayerSlicer
    {
        private readonly Type[] _skipLayers;

        public LayerSlicer(params Type[] skipLayers)
        {
            _skipLayers = skipLayers;
        }

        public IEnumerable<(bool isSkip, IEnumerable<Layer> layers)> GroupLayers(IEnumerable<Layer> layers)
        {
            var slices = SliceLayers(layers);
            var currentSkip = false;
            var currentLayers = new List<Layer>();

            foreach (var slice in slices)
            {
                if (slice.isSkip != currentSkip)
                {
                    if (currentLayers.Any())
                    {
                        yield return (currentSkip, currentLayers);
                    }
                    currentSkip = slice.isSkip;
                    currentLayers = new List<Layer>();
                }
                currentLayers.Add(slice.layer);
            }

            if (currentLayers.Any())
            {
                yield return (currentSkip, currentLayers);
            }
        }

        public IEnumerable<(bool isSkip, Layer layer)> SliceLayers(IEnumerable<Layer> layers)
        {
            foreach (var layer in layers)
            {
                if (_skipLayers.Contains(layer.GetType()))
                {
                    // this is a skip layer
                    yield return (true, layer);
                }
                else
                {
                    var subLayers = SliceLayers(layer.Shapes.OfType<Layer>()).ToArray();
                    if (!subLayers.Any(x => x.isSkip))
                    {
                        // this is a regular layer with no skips inside
                        yield return (false, layer);
                    }
                    else
                    {
                        // this is a regular layer with skips inside
                        IEnumerable<IShape> shapes = layer.Shapes;

                        foreach (var subLayer in subLayers)
                        {
                            var newLayer = layer.Copy();
                            newLayer.Shapes.AddRange(shapes.TakeWhile(s => !IsSameLayerAs(s, subLayer.layer)));
                            shapes = shapes.SkipWhile(s => !IsSameLayerAs(s, subLayer.layer)).Skip(1);
                            if (!subLayer.isSkip)
                            {
                                newLayer.Shapes.Add(subLayer.layer);
                            }
                            if (newLayer.Shapes.Any())
                            {
                                yield return (false, newLayer);
                            }

                            if (subLayer.isSkip)
                            {
                                newLayer = layer.Copy();
                                newLayer.Shapes.Add(subLayer.layer);
                                yield return (true, newLayer);
                            }
                        }
                        if (shapes.Any())
                        {
                            var newLayer = layer.Copy();
                            newLayer.Shapes.AddRange(shapes);
                            yield return (false, newLayer);
                        }
                    }
                }
            }
        }
        
        private static bool IsSameLayerAs(IShape first, IShape second)
        {
            if ((first is Layer firstLayer) && (second is Layer secondLayer))
            {
                return firstLayer.Id == secondLayer.Id;
            }

            return false;
        }
    
    }
}
