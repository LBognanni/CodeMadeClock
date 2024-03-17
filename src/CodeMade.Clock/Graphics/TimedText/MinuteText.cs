using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A text shape that displays the current minute
    /// 
    /// Shares the same properties as the TextShape, however the `Text` property is ignored.
    /// </summary>
    /// <see cref="CodeMade.ScriptedGraphics.TextShape"/>"/>
    /// <example>
	///	{	
	///	    "$type": "MinuteText",
    ///     "FontName": "Tahoma",
    ///     "FontSize": 6,
    ///     "Color": "#cefc",
    ///     "Centered": true,
    ///     "Position": {
    ///         "X": 50,
    ///         "Y": 67
    ///     }
	///	},
    /// </example>
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
