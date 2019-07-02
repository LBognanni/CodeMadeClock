using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class TextShape : IShape
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public int FontSizePx { get; set; }
        public Vertex Position { get; set; }
        public string Color { get; set; }

        public TextShape(string text, string fontName, int fontSizePx, Vertex position, string color)
        {
            Text = text;
            FontName = fontName;
            FontSizePx = fontSizePx;
            Position = position;
            Color = color;
        }

        public void Render(Graphics g)
        {
            using(var textOption = new TextRenderingOption(g, System.Drawing.Text.TextRenderingHint.AntiAlias))
            using (var font = new Font(FontName, FontSizePx, GraphicsUnit.Pixel))
            using (var brush = new SolidBrush(Color.ToColor()))
                g.DrawString(Text, font, brush, Position.AsPointF());
        }
    }
}
