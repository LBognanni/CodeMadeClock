using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock
{
    public class NumbersLayer : Layer
    {
        public int[] Numbers { get; set; }
        public string[] NumbersText { get; set; }
        public bool RotateNumbers { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public float Radius { get; set; }
        public RotateTextMode RotateMode { get; set; } = RotateTextMode.None;
        public bool Is24Hours { get; set; }
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
