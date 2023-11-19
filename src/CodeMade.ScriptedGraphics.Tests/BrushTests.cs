using CodeMade.ScriptedGraphics.Colors;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BrushTests
    {
        [Test]
        public void Parse_Solid_Brush()
        {
            var brushes = "#f00".ParseBrush(new Rectangle());

            CollectionAssert.IsNotEmpty(brushes);
            Assert.AreEqual(1, brushes.Count());

            try
            {
                var brush = brushes.First();
                Assert.IsInstanceOf<BrushOrColoredRegion>(brush);
                Assert.IsTrue(brush.IsBrush);
                Assert.AreEqual(Color.Red.ToArgb(), ((SolidBrush)brush.DangerouslyGetBrush()).Color.ToArgb());
            }
            finally
            {
                brushes.Dispose();
            }
        }

        [Test]
        public void Parse_Gradient_Brush()
        {
            var brushes = "#f00-#00f".ParseBrush(new Rectangle(0, 0, 100, 100));
            CollectionAssert.IsNotEmpty(brushes);
            Assert.AreEqual(1, brushes.Count());

            try
            {
                var brush = brushes.First().DangerouslyGetBrush();
                Assert.IsInstanceOf<LinearGradientBrush>(brush);
                Assert.AreEqual(Color.Red.ToArgb(), ((LinearGradientBrush)brush).LinearColors.First().ToArgb());
                Assert.AreEqual(Color.Blue.ToArgb(), ((LinearGradientBrush)brush).LinearColors.Last().ToArgb());
            }
            finally
            {
                brushes.Dispose();
            }
        }

        [Test]
        public void Parse_Gradient_Brush_WithAngle()
        {
            var brushes = "90-#f00-#00f".ParseBrush(new Rectangle(0, 0, 100, 100));
            CollectionAssert.IsNotEmpty(brushes);
            Assert.AreEqual(1, brushes.Count());

            try
            {
                var brush = brushes.First().DangerouslyGetBrush();
                Assert.IsInstanceOf<LinearGradientBrush>(brush);
                Assert.AreEqual(Color.Red.ToArgb(), ((LinearGradientBrush)brush).LinearColors.First().ToArgb());
                Assert.AreEqual(Color.Blue.ToArgb(), ((LinearGradientBrush)brush).LinearColors.Last().ToArgb());
            }
            finally
            {
                brushes.Dispose();
            }
        }

        [Test]
        public void Throws_On_Invalid_Gradient()
        {
            Assert.Throws<FormatException>(() => { "#f00-f00-f00-f00".ParseBrush(new Rectangle()); });
        }
    }
}
