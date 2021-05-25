using CodeMade.ScriptedGraphics;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeMade.Clock.Tests
{
    class LayerSliceTests
    {
        class WhenNoSkipLayer
        {
            private IEnumerable<(bool isSkip, Layer layer)> _result;

            [SetUp]
            public void Setup()
            {
                _result = (new LayerSlicer(typeof(HoursLayer))).SliceLayers(
                    new[]
                    {
                        MakeLayer(new CircleShape()),
                        MakeLayer(MakeLayer(), MakeLayer(), new CircleShape()),
                        MakeLayer(),
                        MakeLayer()
                    });
            }

            [Test]
            public void It_ShouldGenerate4Layers()
                => Assert.AreEqual(4, _result.Count());

            [Test]
            public void It_ShouldConsiderAllLayersAsNoSkip()
                => CollectionAssert.AreEquivalent(new[] { false, false, false, false }, _result.Select(x => x.isSkip));
        }
        class WhenOneSkipLayer_AtTopLevel
        {
            private IEnumerable<(bool isSkip, Layer layer)> _result;

            [SetUp]
            public void Setup()
            {
                _result = (new LayerSlicer(typeof(HoursLayer))).SliceLayers(
                    new[]
                    {
                        MakeLayer(new CircleShape()),
                        MakeLayer(MakeLayer(), MakeLayer(), new CircleShape()),
                        new HoursLayer(),
                        MakeLayer()
                    });
            }

            [Test]
            public void It_ShouldGenerate4Layers()
                => Assert.AreEqual(4, _result.Count());

            [Test]
            public void It_ShouldConsiderOneLayerAsSkip()
                => CollectionAssert.AreEquivalent(new[] { false, false, true, false }, _result.Select(x => x.isSkip));
        }

        class WhenASkipLayerIsInsideAlayer
        {
            private IEnumerable<(bool isSkip, Layer layer)> _result;

            [SetUp]
            public void Setup()
            {
                var layers = new[]
                {
                    MakeLayer(
                        new CircleShape(),
                        new RectangleShape()
                    ),
                    MakeLayer(
                        new Vertex(10, 20),
                        33,
                        new CircleShape(),
                        MakeLayer(
                            new CircleShape()
                            ),
                        new HoursLayer(),
                        new RectangleShape()
                        ),
                    MakeLayer(
                        new CircleShape(),
                        new MinutesLayer()
                        )
                };

                var slicer = new LayerSlicer(typeof(HoursLayer));
                _result = slicer.SliceLayers(layers);
            }

            [Test]
            public void It_Generates5Slices() =>
                Assert.AreEqual(5, _result.Count());

            [Test]
            public void TheSecondSliceContainsTwoShapes() =>
                Assert.AreEqual(2, _result.Skip(1).First().layer.Shapes.Count);

            [Test]
            public void TheSecondSliceContainsACircle() =>
                Assert.IsInstanceOf<CircleShape>(_result.Skip(1).First().layer.Shapes.First());

            [Test]
            public void TheThirdSliceShouldSkip() =>
                Assert.IsTrue(_result.Skip(2).First().isSkip);

            [Test]
            public void It_DoublesTheLayerThatContainsTheSkipLayer()
            {
                var layers = _result.Select(l => l.layer).ToArray();
                var layerBefore = layers[1];
                var layerAfter = layers[3];
                Assert.AreEqual(layerBefore.Rotate, layerAfter.Rotate);
                Assert.AreEqual(layerBefore.Offset, layerAfter.Offset);
            }
        }

        class WhenASkipLayerIsDeepInsideLayers
        {
            private IEnumerable<(bool isSkip, Layer layer)> _result;

            [SetUp]
            public void Setup()
            {
                var layers = new[]
                {
                    MakeLayer(
                        new CircleShape(),
                        new RectangleShape()
                    ),
                    MakeBlurLayer(
                        MakeLayer(new Vertex(33,12), 45.2f,
                            new CircleShape(),
                            new RectangleShape(),
                            new HoursLayer()
                        )
                    ),
                    MakeLayer(
                        new Vertex(1,2),3,
                        new HoursLayer(),
                        new CircleShape(),
                        new MinutesLayer()
                        )
                };

                var slicer = new LayerSlicer(typeof(HoursLayer));
                _result = slicer.SliceLayers(layers);
            }

            private GaussianBlurLayer MakeBlurLayer(params IShape[] shapes)
            {
                var layer = new GaussianBlurLayer(22.3f);
                layer.Shapes.AddRange(shapes);
                return layer;
            }

            [Test]
            public void It_Generates5Slices() =>
                Assert.AreEqual(5, _result.Count());

            [Test]
            public void TheLastSlice_ShouldHave2Shapes() =>
                Assert.AreEqual(2, _result.Last().layer.Shapes.Count);

            [Test]
            public void It_ShouldSplitTheBlurLayer()
            {
                var layers = _result.Select(x => x.layer).ToArray();
                Assert.IsInstanceOf<GaussianBlurLayer>(layers[1]);
                Assert.IsInstanceOf<GaussianBlurLayer>(layers[2]);
                var blur1 = ((GaussianBlurLayer)layers[1]);
                var blur2 = ((GaussianBlurLayer)layers[2]);

                var expected1 =
                    MakeBlurLayer(
                        MakeLayer(
                            new Vertex(33, 12), 45.2f,
                            new CircleShape(),
                            new RectangleShape()
                        )
                    );
                blur1.Should().BeEquivalentTo(expected1, options => options.Excluding(x => x.Id));

                var expected2 =
                    MakeBlurLayer(
                        MakeLayer(
                            new Vertex(33, 12), 45.2f,
                            new HoursLayer()
                        )
                    );
                blur2.Should().BeEquivalentTo(expected2, options => options.Excluding(x => x.Id));
            }
        }

        public class WithManyScenarios
        {
            [Test]
            [TestCaseSource(nameof(GenerateTestCases))]
            public void It_ShouldSplitAsExpected(IEnumerable<Layer> layers, IEnumerable<(bool, Layer)> expected)
            {
                var slicer = new LayerSlicer(typeof(HoursLayer));
                var result = slicer.SliceLayers(layers).ToArray();
                result.Should().BeEquivalentTo(expected, options=> options.Excluding(x=>x.Item2.Id));
            }

            public static IEnumerable<object[]> GenerateTestCases
            {
                get
                {
                    yield return new object[]
                    {
                        new[]
                        {
                            MakeLayer()
                        },
                        new[]
                        {
                            (false, MakeLayer())
                        }
                    };

                    yield return new object[]
                    {
                        new Layer[]
                        {
                            new HoursLayer()
                        },
                        new (bool, Layer)[]
                        {
                            (true, new HoursLayer())
                        }
                    };

                    yield return new object[]
                    {
                        new[]
                        {
                            new Layer(),
                            new HoursLayer()
                        },
                        new[]
                        {
                            (false, new Layer()),
                            (true, new HoursLayer())
                        }
                    };

                    yield return  new object[]
                    {
                        new []
                        {
                            MakeLayer(),
                            MakeLayer(
                    MakeLayer(
                        new HoursLayer(),
                                    new HoursLayer(),
                                    new CircleShape()
                                )
                            )
                        },
                        new []
                        {
                            (false, MakeLayer()),
                            (true,  MakeLayer(MakeLayer(new HoursLayer()))),
                            (true,  MakeLayer(MakeLayer(new HoursLayer()))),
                            (false, MakeLayer(MakeLayer(new CircleShape())))
                        }
                    };

                    yield return new object[]
                    {
                        new []
                        {
                            MakeLayer(
                                new Vertex(11,22),33,
                                MakeLayer(new Vertex(1,2), 3,
                                    new HoursLayer(),
                                    new CircleShape()
                                )
                            )
                        },
                        new []
                        {
                            (
                                true,
                                MakeLayer(
                                    new Vertex(11,22),33,
                                    MakeLayer(new Vertex(1,2),3, new HoursLayer()))
                            ),
                            (
                                false,
                                MakeLayer(
                                    new Vertex(11,22),33,
                                    MakeLayer(new Vertex(1,2),3, new CircleShape()))
                            ),
                        }
                    };

                    yield return new object[]
                    {
                        new []
                        {
                            MakeLayer(
                                new Vertex(11,22),33,
                                MakeLayer(new Vertex(1,2), 3,
                                    new CircleShape(),
                                    new HoursLayer()
                                )
                            )
                        },
                        new []
                        {
                            (
                                true,
                                MakeLayer(
                                    new Vertex(11,22),33,
                                    MakeLayer(new Vertex(1,2),3, new HoursLayer()))
                            ),
                            (
                                false,
                                MakeLayer(
                                    new Vertex(11,22),33,
                                    MakeLayer(new Vertex(1,2),3, new CircleShape()))
                            ),
                        }
                    };

                    yield return new object[]
                    {
                        new []
                        {
                            MakeLayer(),
                            MakeLayer(),
                            MakeLayer(
                                MakeLayer(
                                    new HoursLayer(),
                                    new HoursLayer()
                                    ),
                                MakeLayer(
                                    new HoursLayer(),
                                    new HoursLayer()
                                ),
                                new CircleShape()
                            ),
                        },
                        new []
                        {
                            (false, MakeLayer()),
                            (false, MakeLayer()),
                            (false, MakeLayer()),
                            (true, MakeLayer(MakeLayer(new HoursLayer()))),
                            (true, MakeLayer(MakeLayer(new HoursLayer()))),
                            (true, MakeLayer(MakeLayer(new HoursLayer()))),
                            (true, MakeLayer(MakeLayer(new HoursLayer()))),
                            (false, MakeLayer(new CircleShape())),
                        }
                    };
                }
            }
        }

        public class WhenGrouping
        {
            private IEnumerable<(bool isSkip, IEnumerable<Layer> layers)> _groups;

            [SetUp]
            public void Setup()
            {
                var layers = new []
                {
                    MakeLayer(),
                    MakeLayer(),
                    MakeLayer(),
                    MakeLayer(),
                    new HoursLayer(),
                    MakeLayer(),
                    MakeLayer(
                        MakeLayer(),
                        new HoursLayer(),
                        MakeLayer()
                        ),
                    MakeLayer(),
                    MakeLayer(),
                };

                _groups = (new LayerSlicer(typeof(HoursLayer)).GroupLayers(layers));
            }

            [Test]
            public void It_ShouldCreate5Groups() =>
                _groups.Count().Should().Be(5);

            [Test]
            public void It_ShouldCreateTheRightTypeOfGroups() =>
                _groups.Select(x => x.isSkip).Should().BeEquivalentTo(new[]
                {
                    false, true, false, true, false
                });
        }

        private static Layer MakeLayer(params IShape[] shapes)
        {
            var layer = new Layer();
            layer.Shapes.AddRange(shapes);
            return layer;
        }

        private static Layer MakeLayer(Vertex offset, float rotation, params IShape[] shapes)
        {
            var layer = new Layer
            {
                Rotate = rotation,
                Offset= offset
            };
            layer.Shapes.AddRange(shapes);
            return layer;
        }
    }
}
