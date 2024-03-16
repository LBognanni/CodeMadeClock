using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    public class HourText : TimedText
    {
        public HourText(IFileReader fileReader) : base(fileReader)
        {
        }

        public HourText(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color) : base(fileReader, text, fontName, fontSizePx, position, color)
        {
        }

        public override void Update(LocalTime time)
        {
            Text = time.Hour.ToString();
        }
    }
}
