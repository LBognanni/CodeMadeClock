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
            var slices = SliceLayers(layers).ToList();
            var currentLayers = new List<Layer>();

            foreach (var slice in slices)
            {
                if (slice.isSkip)
                {
                    if (currentLayers.Any())
                    {
                        yield return (false, currentLayers);
                        currentLayers = new List<Layer>();
                    }

                    yield return (true, new[] { slice.layer });
                }
                else
                {
                    currentLayers.Add(slice.layer);
                }
            }

            if (currentLayers.Any())
            {
                yield return (false, currentLayers);
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
                    var subLayers = SliceLayers(layer.Shapes.OfType<Layer>()).ToList();
                    if (!subLayers.Any(x => x.isSkip))
                    {
                        // this is a regular layer with no skips inside
                        yield return (false, layer);
                    }
                    else
                    {
                        // this is a regular layer with skips inside
                        IEnumerable<IShape> shapes = layer.Shapes;

                        var newLayer = layer.Copy();
                        foreach (var shape in shapes)
                        {
                            if (shape is Layer layerShape)
                            {
                                var subTuples = subLayers.Where(x => AreTheSameLayer(x.layer, layerShape)).ToList();
                                foreach (var subTuple in subTuples)
                                {
                                    if (subTuple.isSkip)
                                    {
                                        if (newLayer.Shapes.Any())
                                        {
                                            yield return (false, newLayer);
                                        }

                                        newLayer = layer.Copy();
                                        newLayer.Shapes.Add(subTuple.layer);
                                        yield return (true, newLayer);
                                        newLayer = layer.Copy();

                                    }
                                    else
                                    {
                                        newLayer.Shapes.Add(subTuple.layer);
                                    }

                                    subLayers.Remove(subTuple);
                                }
                            }
                            else
                            {
                                newLayer.Shapes.Add(shape);
                            }
                        }
                        if (newLayer.Shapes.Any())
                        {
                            yield return (false, newLayer);
                        }
                    }
                }
            }
        }
        
        private static bool AreTheSameLayer(IShape first, IShape second)
        {
            if ((first is Layer firstLayer) && (second is Layer secondLayer))
            {
                return firstLayer.Id == secondLayer.Id;
            }

            return false;
        }
    
    }
}
