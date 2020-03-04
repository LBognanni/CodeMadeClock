using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.Clock
{
    public class NumbersLayer : Layer
    {
        public enum RotateTextMode
        {
            None,
            RotateIn,
            RotateOut
        }

        class TextShapeWithRotation : TextShape
        {
            public TextShapeWithRotation(string text, string fontName, int fontSizePx, Vertex position, string color) 
                : base(text, fontName, fontSizePx, position, color)
            {
            }

            public float Rotation { get; set; }

            protected override void RenderString(Graphics g, Font font, SolidBrush brush, PointF position, Vertex move)
            {
                var container = g.BeginContainer();
                
                g.TranslateTransform(position.X, position.Y);
                g.RotateTransform(Rotation);
                g.TranslateTransform(-move.X, -move.Y);

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(Text, font, brush, 0, 0);
                
                g.EndContainer(container);
            }
        }

        public int[] Numbers { get; set; }
        public string[] NumbersText { get; set; }
        public bool RotateNumbers { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public float Radius { get; set; }
        public RotateTextMode RotateMode { get; set; } = RotateTextMode.None;

        private Lazy<TextShapeWithRotation[]> _textShapes;

        public NumbersLayer()
        {
            _textShapes = new Lazy<TextShapeWithRotation[]>(TextShapeFactory);
        }

        private TextShapeWithRotation[] TextShapeFactory()
        {
            List<TextShapeWithRotation> shapes = new List<TextShapeWithRotation>();
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

                    float rotation = RotateMode switch
                    {
                        RotateTextMode.RotateIn => hour * 30,
                        RotateTextMode.RotateOut => 180.0f + (hour * 30) ,
                        _ => 0
                    };

                    shapes.Add(new TextShapeWithRotation(text, FontName, FontSize, pos, Color) { Centered = true, Rotation = rotation });
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
