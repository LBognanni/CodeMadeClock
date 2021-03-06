﻿using NUnit.Framework;

namespace CodeMade.ScriptedGraphics.Tests
{
    public class TextTests : BitmapTesterBase
    {
        [Test]
        public void When_Writing_Text()
        {
            Canvas canvas = new Canvas(100, 20, "white");
            canvas.Add(new TextShape(null, "Testing 123", "Arial", 16, new Vertex(0, 0), "black"));
            AssertBitmapsAreEqual(LoadLocalBitmap(@"testimages\text.png"), canvas.Render());
        }
    }
}
