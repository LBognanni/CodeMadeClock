using CodeMade.ScriptedGraphics;

namespace CodeMade.Clock
{
    public abstract class TimedText : TimedLayer, ITextShape
    {
        private TextShape _textShape;

        public TimedText(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color)
        {
            _textShape = new TextShape(fileReader, text, fontName, fontSizePx, position, color);
        }

        public TimedText(IFileReader fileReader)
        {
            _textShape = new TextShape(fileReader);
        }

        public bool Centered { get => _textShape.Centered ; set => _textShape.Centered  = value; }
        public string Color { get => _textShape.Color ; set => _textShape.Color  = value; }
        public string Font { get => _textShape.Font ; set => _textShape.Font  = value; }
        public string FontFile { get => _textShape.FontFile ; set => _textShape.FontFile  = value; }
        public string FontName { get => _textShape.FontName ; set => _textShape.FontName  = value; }
        public float FontSize { get => _textShape.FontSize ; set => _textShape.FontSize  = value; }
        public float FontSizePx { get => _textShape.FontSizePx ; set => _textShape.FontSizePx  = value; }
        public Vertex Position { get => _textShape.Position ; set => _textShape.Position  = value; }
        public string Text { get => _textShape.Text; set => _textShape.Text = value; }

        public override void Render(System.Drawing.Graphics g, float scaleFactor = 1) => _textShape.Render(g, scaleFactor);

        public void Dispose() => _textShape.Dispose();
    }
}
