using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// Draws a string of text at the specified coordinates.
    /// The text will be either printed using a system font specified in `Font` or a specific .ttf font file specified in `FontFile`
    /// </summary>
    /// <example>       
    /// {
    ///     "$type": "TextShape",
    ///     "Text": "codemade.net",
    ///     "FontName": "Tahoma",
    ///     "FontSizePx": 6,
    ///     "Color": "#cefc",
    ///     "Centered": true,
    ///     "Position": {
    ///         "X": 50,
    ///         "Y": 67
    ///     }
    /// }
    /// </example>
    public class TextShape : IShape, IDisposable
    {
        private readonly PrivateFontCollection _fontCollection;
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Text to print
        /// </summary>
        public string Text { get; set; }

        public string FontName { get; set; }
        public float FontSizePx { get; set; }
        public Vertex Position { get; set; }

        /// <summary>
        /// Color of the text. It should be a simple color, gradients are not supported.
        /// </summary>
        /// <see cref="Colors"/>
        public string Color { get; set; }
        /// <summary>
        /// `true` if the text should be centered at `Position`, false if it shoud begin at `Position`
        /// </summary>
        public bool Centered { get; set; }
        /// <summary>
        /// Name of the font. It should be a font that is installed in the system
        /// If `FontFile` is specified, this will be ignored.
        /// </summary>
        public string Font { get => FontName; set => FontName = value; }

        /// <summary>
        /// Font size
        /// </summary>
        public float FontSize { get => FontSizePx; set => FontSizePx = value; }

        /// <summary>
        /// Name of a custom font file that should be redistributed with this skin.
        /// If using this, the `Font` property is ignored
        /// </summary>
        public string FontFile { get; set; }

        public TextShape(IFileReader fileReader)
        {
            _fontCollection = new PrivateFontCollection();
            _fileReader = fileReader;
        }

        public TextShape(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color)
        {
            _fontCollection = new PrivateFontCollection();
            _fileReader = fileReader;
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


        protected Font GetFont(float scaleFactor)
        {
            if(!string.IsNullOrEmpty(FontFile))
            {
                return GetCustomFont(scaleFactor);
            }

            return GetInstalledFont(scaleFactor);
        }

        protected Font GetCustomFont(float scaleFactor)
        {
            if (_fontCollection.Families.Length == 0)
            {
                _fontCollection.AddFontFile(_fileReader.GetFontFile(FontFile));
            }

            return new Font(_fontCollection.Families.First(), FontSize * scaleFactor);
        }

        protected Font GetInstalledFont(float scaleFactor)
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

        public void Dispose()
        {
            _fontCollection.Dispose();
        }
    }
}
