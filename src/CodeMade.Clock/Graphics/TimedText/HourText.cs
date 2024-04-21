using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A text shape that displays the current hour
    /// 
    /// Shares the same properties as the TextShape, however the `Text` property is ignored.
    /// </summary>
    /// <see cref="CodeMade.ScriptedGraphics.TextShape"/>"/>
    /// <example>
	///	{	
	///	    "$type": "HourText",
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
            Text = time.Hour.ToString(Format);
        }
    }
}
