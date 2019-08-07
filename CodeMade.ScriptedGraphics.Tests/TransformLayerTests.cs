﻿using NUnit.Framework;
using System.Linq;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class TransformLayerTests : BitmapTesterBase
    {
        [Test]
        public void Rotate45Degrees_rect()
        {
            Canvas canvas = new Canvas(100, 100, "white");
            canvas.Add(new RectangleShape(-25, -25, 50, 50, "#000"));
            canvas.Layers.Last().Rotate = 45;
            canvas.Layers.Last().Offset = new Vertex(50, 50);

            canvas.Layers.Add(new Layer());
            canvas.Add(new RectangleShape(25, 25, 50, 50, "red"));

            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\rotate45deg.png"), canvas.Render());
        }
    }
}
