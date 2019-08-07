using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class BrushTests
    {
        [Test]
        public void Parse_Solid_Brush()
        {
            var brush = "#f00".ParseBrush(new Rectangle());
            try
            {
                Assert.IsInstanceOf<SolidBrush>(brush);
                Assert.AreEqual(Color.Red.ToArgb(), ((SolidBrush)brush).Color.ToArgb());
            }
            finally
            {
                brush.Dispose();
            }
        }

        [Test]
        public void Parse_Gradient_Brush()
        {
            var brush = "#f00-#00f".ParseBrush(new Rectangle(0, 0, 100, 100));
            try
            {
                Assert.IsInstanceOf<LinearGradientBrush>(brush);
                Assert.AreEqual(Color.Red.ToArgb(), ((LinearGradientBrush)brush).LinearColors.First().ToArgb());
                Assert.AreEqual(Color.Blue.ToArgb(), ((LinearGradientBrush)brush).LinearColors.Last().ToArgb());
            }
            finally
            {
                brush.Dispose();
            }
        }

        [Test]
        public void Parse_Gradient_Brush_WithAngle()
        {
            var brush = "90-#f00-#00f".ParseBrush(new Rectangle(0, 0, 100, 100));
            try
            {
                Assert.IsInstanceOf<LinearGradientBrush>(brush);
                Assert.AreEqual(Color.Red.ToArgb(), ((LinearGradientBrush)brush).LinearColors.First().ToArgb());
                Assert.AreEqual(Color.Blue.ToArgb(), ((LinearGradientBrush)brush).LinearColors.Last().ToArgb());
            }
            finally
            {
                brush.Dispose();
            }
        }

        [Test]
        public void Throws_On_Invalid_Gradient()
        {
            Assert.Throws<FormatException>(() => { "#f00-f00-f00-f00".ParseBrush(new Rectangle()); });
        }
    }
}
