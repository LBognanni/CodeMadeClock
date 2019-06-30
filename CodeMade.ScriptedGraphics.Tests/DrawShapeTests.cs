using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics.Tests
{
    class DrawShapeTests : BitmapTesterBase
    {
        protected IEnumerable<Vertex> VertexArrayFromString(string path)
        {
            const string dividers = ", ";
            var numbers = path.Split(dividers.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            Vertex v = null;
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    v = new Vertex() { X = float.Parse(numbers[i]) };
                }
                else
                {
                    v.Y = float.Parse(numbers[i]);
                    yield return v;
                }
            }
        }

        [Test]
        public void When_Parsing_Path_Returns_Correct_Points()
        {
            var vertices = VertexArrayFromString("1,1 1,2 2,2").ToArray();
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
            var vertices = VertexArrayFromString("1,1 1,2 2,2 44").ToArray();
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
            Assert.IsTrue(AreBitmapsEqual(LoadLocalBitmap("testimages/black10x10.png"), (Bitmap)img));
        }
    }
}
