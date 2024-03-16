using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    public class MinuteText : TimedText
    {
        public MinuteText(IFileReader fileReader) : base(fileReader)
        {
        }

        public MinuteText(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color) : base(fileReader, text, fontName, fontSizePx, position, color)
        {
        }

        public override void Update(LocalTime time)
        {
            Text = time.Minute.ToString();
        }
    }
}
