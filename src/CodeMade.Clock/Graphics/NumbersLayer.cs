using CodeMade.ScriptedGraphics;
using CodeMade.ScriptedGraphics.Colors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock
{
    /// <summary>
    /// Use this layer to draw the hours numbers on your clock
    /// </summary>
    /// <example>
    /// {
    /// 	"$type": "NumbersLayer",
    /// 	"Numbers": [
    /// 		3,
    /// 		6,
    /// 		9,
    /// 		12
    /// 	],
    /// 	"NumbersText": [
    /// 	    "III",
    /// 	    "VI",
    /// 	    "IX",
    /// 	    "XII"
    /// 	],
    /// 	"Color": "#cedf",
    /// 	"FontName": "Times New Roman",
    /// 	"FontSize": 16,
    /// 	"Radius": 35,
    /// 	"Offset": {
    /// 		"X": 52,
    /// 		"Y": 52.5
    /// 	}
    /// }
    /// </example>
    public class NumbersLayer : Layer
    {
        /// <summary>
        /// [Optional] An array of numbers that will be drawn. Default value is all hours (`[1,2,3,4,5,6,7,8,9,10,11,12]`)
        /// </summary>
        public int[] Numbers { get; set; }

        /// <summary>
        /// [Optional] An array of strings that corresponds to the text shown for each hour defined in `Numbers`.
        /// Default value is the same as `Numbers`
        /// </summary>
        public string[] NumbersText { get; set; }
        
        /// <summary>
        /// Name of the font to be used to render `NumbersText`
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// Size of the font
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Text color. Cannot be a gradient.
        /// </summary>
        /// <see cref="CodeMade.ScriptedGraphics.Colors.Colors"/>
        public string Color { get; set; }

        /// <summary>
        /// The radius 
        /// ```
        ///           12
        ///          /|    1
        ///    radius |     
        ///         \ |       2
        ///          \|
        /// 9---------+---------3
        ///           |
        ///           |
        ///           |
        ///           |
        ///           6
        /// 
        /// ```
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// [Optional] How the text is rotated. Can be one of the following values:
        ///  - `0` (default) - No rotation
        ///  - `1` - Text is rotated so that it faces inside the center
        ///  - `2` - Text is rotated so that it faces outside the center
        /// </summary>
        public RotateTextMode RotateMode { get; set; } = RotateTextMode.None;

        /// <summary>
        /// `true` if you want to have a 24-hour dial
        /// </summary>
        public bool Is24Hours { get; set; }

        /// <summary>
        /// [Optional] Alternative to `FontName`, you can specify a custom .ttf font file
        /// </summary>
        /// <see cref="TextShape"></see>
        public string FontFile { get; set; }

        private Lazy<TextShapeWithRotation[]> _textShapes;
        private Graphics _graphics;
        private readonly IFileReader _fileReader;

        public NumbersLayer(IFileReader fileReader)
        {
            _textShapes = new Lazy<TextShapeWithRotation[]>(TextShapeFactory);
            _fileReader = fileReader;
        }

        private TextShapeWithRotation[] TextShapeFactory()
        {
            List<TextShapeWithRotation> shapes = new List<TextShapeWithRotation>();
            const double hourInRad = Math.PI / 6;
            const double offsetInRad = hourInRad * -3;
            var numbers = Numbers ?? Enumerable.Range(1, Is24Hours ? 24 : 12).ToArray();

            for (int hour = 1; hour <= (Is24Hours ? 24 : 12); ++hour)
            {
                var numberIdx = Array.IndexOf(numbers, hour);
                if (numberIdx >= 0)
                {
                    var angle = (Is24Hours ? hourInRad / 2 : hourInRad) * hour + offsetInRad;
                    var pos = new Vertex((float)Math.Cos(angle) * Radius, (float)Math.Sin(angle) * Radius);

                    string text = hour.ToString();
                    if ((NumbersText?.Length ?? 0) > numberIdx)
                    {
                        text = NumbersText[numberIdx];
                    }

                    var baseRotation = hour * (Is24Hours ? 15 : 30);

                    var rotation = RotateMode switch
                    {
                        RotateTextMode.RotateIn => baseRotation,
                        RotateTextMode.RotateOut => 180.0f + baseRotation,
                        RotateTextMode.RotateBegin => 270 + baseRotation,
                        RotateTextMode.RotateEnd => 90 + baseRotation,
                        _ => 0
                    };

                    shapes.Add(
                        new TextShapeWithRotation(_fileReader, text, FontName, FontSize, pos, Color) { 
                            Centered = true, 
                            Rotation = rotation, 
                            RotateMode = RotateMode,
                            FontFile = FontFile
                        });
                }
            }

            return shapes.ToArray();
        }

        protected override void RenderShapes(Graphics g, float scaleFactor)
        {
            _graphics = g;

            foreach (var shape in _textShapes.Value)
            {
                shape.Render(g, scaleFactor);
            }
        }
    }
}
