using CodeMade.ScriptedGraphics.Colors;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class ColorTests
    {
        [Test]
        public void When_Color_is_unknown_should_Throw()
        {
            Assert.Throws<FormatException>(() => "#defff".ToColor());
        }

        [Test]
        public void Parse_Named_Color_Red()
        {
            Assert.AreEqual(Color.Red, "red".ToColor());
            Assert.AreEqual(Color.Red, "Red".ToColor());
        }

        [Test]
        public void Convert_Color_To_Html()
        {
            Assert.AreEqual("#ff0000ff", Color.Red.ToHtml());
            Assert.AreEqual("#008000ff", Color.Green.ToHtml());
            Assert.AreEqual("#0000ffff", Color.Blue.ToHtml());
        }

        [Test]
        public void Parse_Html_Color()
        {
            Assert.AreEqual(Color.Red.ToArgb(), "#f00".ToColor().ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), "#f00f".ToColor().ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), "#ff0000".ToColor().ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), "#ff0000ff".ToColor().ToArgb());

            Assert.AreEqual(Color.Blue.ToArgb(), "#0000ffff".ToColor().ToArgb());
            Assert.AreEqual(Color.Green.ToArgb(), "#008000ff".ToColor().ToArgb());
        }

        [TestCase("en-US")]
        [TestCase("it-IT")]
        [TestCase("fr-FR")]
        public void Parse_Gradients(string threadCulture)
        {
            var culture = new CultureInfo(threadCulture);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var  color = Colors.Colors.ParseBrush("(0.5,0.5)-red-green-blue", new RectangleF(0, 0, 100, 100));
        }
    }
}
