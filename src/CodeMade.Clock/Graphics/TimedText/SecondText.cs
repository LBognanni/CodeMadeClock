using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A text shape that displays the current second
    /// 
    /// Shares the same properties as the TextShape, however the `Text` property is ignored.
    /// </summary>
    /// <see cref="CodeMade.ScriptedGraphics.TextShape"/>"/>
    /// <example>
	///	{	
	///	    "$type": "SecondText",
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
    public class SecondText : TimedText
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
