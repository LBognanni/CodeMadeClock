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

        private Lazy<TextShape[]> _textShapes;

        public NumbersLayer()
        {
            _textShapes = new Lazy<TextShape[]>(TextShapeFactory);
        }

        private TextShape[] TextShapeFactory()
        {
            List<TextShape> shapes = new List<TextShape>();
            const double hourInRad = Math.PI / 6;
            const double offsetInRad = hourInRad * -3;

            for (int hour = 1; hour <= 12; ++hour)
            {
                var numberIdx = Array.IndexOf(Numbers, hour);
                if (numberIdx >= 0)
                {
                    var angle = hourInRad * hour + offsetInRad;
                    var pos = new Vertex((float)Math.Cos(angle) * Radius, (float)Math.Sin(angle) * Radius);

                    string text = hour.ToString();
                    if ((NumbersText?.Length ?? 0) > numberIdx)
                    {
                        text = NumbersText[numberIdx];
                    }

                    shapes.Add(new TextShape(text, FontName, FontSize, pos, Color) { Centered = true });
                }
            }

            return shapes.ToArray();
        }

        protected override void RenderShapes(Graphics g, float scaleFactor)
        {
            foreach(var shape in _textShapes.Value)
            {
                shape.Render(g, scaleFactor);
            }
        }
    }
}
