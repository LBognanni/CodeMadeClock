using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    public class TextShape : IShape
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public float FontSizePx { get; set; }
        public Vertex Position { get; set; }
        public string Color { get; set; }
        public bool Centered { get; set; }

        public string Font { get => FontName; set => FontName = value; }
        public float FontSize { get => FontSizePx; set => FontSizePx = value; }

        public TextShape()
        {
        }

        public TextShape(string text, string fontName, int fontSizePx, Vertex position, string color)
        {
            Text = text;
            FontName = fontName;
            FontSizePx = fontSizePx;
            Position = position;
            Color = color;
        }

        public virtual void Render(Graphics g, float scaleFactor)
        {
            using (var textOption = new TextRenderingOption(g, System.Drawing.Text.TextRenderingHint.AntiAlias))
            using (var font = GetFont(scaleFactor))
            using (var brush = new SolidBrush(Color.ToColor()))
            {
                var position = Position.AsPointF(scaleFactor);
                Vertex move = new Vertex();
                if (Centered)
                {
                    var sz = g.MeasureString(Text, font);
                    move = new Vertex(sz.Width / 2, sz.Height / 2);
                }
                RenderString(g, font, brush, position, move);
            }
        }

        protected virtual Font GetFont(float scaleFactor)
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

            return new Font(string.Join(" ", fontNameList.ToArray()), FontSizePx * scaleFactor, style, GraphicsUnit.Pixel);
        }

        protected virtual void RenderString(Graphics g, Font font, SolidBrush brush, PointF position, Vertex move)
        {
            g.DrawString(Text, font, brush, position.X - move.X, position.Y - move.Y);
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
