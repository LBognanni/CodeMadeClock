using NUnit.Framework;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CodeMade.ScriptedGraphics.Tests
{
    class DrawShapeTests : BitmapTesterBase
    {

        [Test]
        public void When_Parsing_Path_Returns_Correct_Points()
        {
            var vertices = Shape.VertexArrayFromString("1,1 1,2 2,2").ToArray();
            Assert.AreEqual(3, vertices.Length);
            Assert.AreEqual(1, vertices[0].X);
            Assert.AreEqual(1, vertices[0].Y);
            Assert.AreEqual(1, vertices[1].X);
            Assert.AreEqual(2, vertices[1].Y);
            Assert.AreEqual(2, vertices[2].X);
            Assert.AreEqual(2, vertices[2].Y);
        }

        [Test]
        public void When_Parsing_Invalid_Path_Returns_Found_Points()
        {
            var vertices = Shape.VertexArrayFromString("1,1 1,2 2,2 44").ToArray();
            Assert.AreEqual(3, vertices.Length);
            Assert.AreEqual(1, vertices[0].X);
            Assert.AreEqual(1, vertices[0].Y);
            Assert.AreEqual(1, vertices[1].X);
            Assert.AreEqual(2, vertices[1].Y);
            Assert.AreEqual(2, vertices[2].X);
            Assert.AreEqual(2, vertices[2].Y);
        }

        [Test]
        public void when_drawing_a_rectangle()
        {
            var shape = new RectangleShape(0, 0, 10, 10, "#000");

            var canvas = new Canvas(10, 10, "#fff");
            canvas.Add(shape);

            var img = canvas.Render();
            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\black10x10.png"), img);
        }

        [Test]
        public void when_drawing_a_circle()
        {
            var shape = new CircleShape(99.5f, 99.5f, 99.5f, "#000");

            var canvas = new Canvas(200, 200, "#fff");
            canvas.Add(shape);

            var img = canvas.Render();
            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\blackcircle.png"), img);
        }

        class PixelPerfectLayer : Layer
        {
            protected override void BeforeRenderShapes(Graphics g, float scaleFactor)
            {
                g.SmoothingMode = SmoothingMode.Default;
            }
        }

        [Test]
        public void when_drawing_A_shape()
        {
            Bitmap bitmapShape = When_Drawing_A_Shape_Render_A_Rectangle_Shape();
            Bitmap bitmapRectangle = When_Drawing_A_Shape_Render_A_Rectangle();

            AssertBitmapsAreEqual(bitmapRectangle, bitmapShape);
        }

        private Bitmap When_Drawing_A_Shape_Render_A_Rectangle_Shape()
        {
            Canvas canvas = new Canvas(100, 100, "white");
            canvas.Layers.Add(new PixelPerfectLayer());

            canvas.Add(new Shape
            {
                Color = "#000",
                Vertices = Shape.VertexArrayFromString("0,0 10,0, 10,10, 0,10")
            });
            var bitmapShape = canvas.Render();
            return bitmapShape;
        }

        private static Bitmap When_Drawing_A_Shape_Render_A_Rectangle()
        {
            Canvas controlCanvas = new Canvas(100, 100, "white");
            controlCanvas.Add(new RectangleShape(0, 0, 10, 10, "#000"));
            var bitmapRectangle = controlCanvas.Render();
            return bitmapRectangle;
        }

        [Test]
        public void DonutShape_Is_Like_Two_Circles()
        {
            var canvas1 = new Canvas(100, 100, "#0000");
            canvas1.Add(new RectangleShape(0, 0, 100, 100, "white"));
            canvas1.Add(new CircleShape(50, 50, 40, "red"));
            canvas1.Add(new CircleShape(50, 50, 20, "white"));

            var canvas2 = new Canvas(100, 100, "#0000");
            canvas2.Add(new RectangleShape(0, 0, 100, 100, "white"));
            canvas2.Add(new DonutShape { Position = new Vertex(50, 50), Radius = 40, InnerRadius = 20, Color = "red" });

            AssertBitmapsAreEqual(canvas1.Render(), canvas2.Render(), PixelCompareMode.RGBSimilar);
        }

        [Test]
        [Ignore("inner circle is aliased using DeleteCircleShape")]
        public void DonutShape_Is_Like_Circle_Minus_Erase()
        {
            var canvas1 = new Canvas(100, 100, "#0000");
            canvas1.Add(new CircleShape(50, 50, 40, "red"));
            canvas1.Add(new DeleteCircleShape { Position = new Vertex(50, 50), Radius = 20 });

            var canvas2 = new Canvas(100, 100, "#0000");
            canvas2.Add(new DonutShape { Position = new Vertex(50, 50), Radius = 40, InnerRadius = 20, Color = "red" });

            AssertBitmapsAreEqual(canvas1.Render(), canvas2.Render(), PixelCompareMode.RGB);
        }
    }
}
