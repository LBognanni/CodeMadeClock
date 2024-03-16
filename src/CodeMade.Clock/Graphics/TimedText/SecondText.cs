using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    internal class SecondText : TimedText
    {
        public SecondText(IFileReader fileReader) : base(fileReader)
        {
        }

        public SecondText(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color) : base(fileReader, text, fontName, fontSizePx, position, color)
        {
        }

        public override void Update(LocalTime time)
        {
            Text = time.Second.ToString();
        }
    }
}
