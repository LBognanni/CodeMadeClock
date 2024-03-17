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

        /// <summary>
        /// `true` if the text should be centered at `Position`, false if it shoud begin at `Position`
        /// </summary>
        public bool Centered { get => _textShape.Centered ; set => _textShape.Centered  = value; }

        /// <summary>
        /// Color of the text. It should be a simple color, gradients are not supported.
        /// </summary>
        /// <see cref="CodeMade.ScriptedGraphics.Colors.Colors"/>
        public string Color { get => _textShape.Color ; set => _textShape.Color  = value; }

        /// <summary>
        /// Name of the font. It should be a font that is installed in the system
        /// If `FontFile` is specified, this will be ignored.
        /// </summary>
        public string Font { get => _textShape.Font ; set => _textShape.Font  = value; }

        /// <summary>
        /// Name of a custom font file that should be redistributed with this skin.
        /// If using this, the `Font` property is ignored
        /// </summary>
        public string FontFile { get => _textShape.FontFile ; set => _textShape.FontFile  = value; }
        public string FontName { get => _textShape.FontName ; set => _textShape.FontName  = value; }

        /// <summary>
        /// Font size
        /// </summary>
        public float FontSize { get => _textShape.FontSize ; set => _textShape.FontSize  = value; }
        public float FontSizePx { get => _textShape.FontSizePx ; set => _textShape.FontSizePx  = value; }
        public Vertex Position { get => _textShape.Position ; set => _textShape.Position  = value; }

        public string Text { get => _textShape.Text; set => _textShape.Text = value; }

        public override void Render(System.Drawing.Graphics g, float scaleFactor = 1) => _textShape.Render(g, scaleFactor);

        public void Dispose() => _textShape.Dispose();
    }
}
