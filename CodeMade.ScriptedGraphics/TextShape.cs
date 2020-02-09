﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    public class TextShape : IShape
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public int FontSizePx { get; set; }
        public Vertex Position { get; set; }
        public string Color { get; set; }
        public bool Centered { get; set; }

        public TextShape(string text, string fontName, int fontSizePx, Vertex position, string color)
        {
            Text = text;
            FontName = fontName;
            FontSizePx = fontSizePx;
            Position = position;
            Color = color;
        }

        public void Render(Graphics g, float scaleFactor)
        {
            var fontNameList = FontName.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            bool isBold = FindAndRemove(fontNameList, "Bold");
            bool isItalic = FindAndRemove(fontNameList, "Italic");
            FontStyle style = FontStyle.Regular;
            if (isBold)
            {
                style |= FontStyle.Bold;
            }

            if (isItalic)
            {
                style |= FontStyle.Italic;
            }

            using (var textOption = new TextRenderingOption(g, System.Drawing.Text.TextRenderingHint.AntiAlias))
            using (var font = new Font(string.Join(" ", fontNameList.ToArray()), FontSizePx * scaleFactor, style, GraphicsUnit.Pixel))
            using (var brush = new SolidBrush(Color.ToColor()))
            {
                var position = Position.AsPointF(scaleFactor);
                if(Centered)
                {
                    var sz = g.MeasureString(Text, font);
                    position = position.Minus(sz.Width / 2, sz.Height / 2);
                }
                g.DrawString(Text, font, brush, position);
            }
        }

        private bool FindAndRemove(List<string> fontNameList, string find)
        {
            var idx = fontNameList.IndexOf(find);
            if (idx == -1)
                return false;

            fontNameList.RemoveAt(idx);
            return true;
        }
    }
}
