using CodeMade.ScriptedGraphics;
using System.Drawing;

namespace CodeMade.Clock
{
    public enum RotateTextMode
    {
        None,
        RotateIn,
        RotateOut,
        RotateBegin,
        RotateEnd
    }

    public class TextShapeWithRotation : TextShape
    {

        public TextShapeWithRotation(IFileReader fileReader, string text, string fontName, int fontSizePx, Vertex position, string color)
            : base(fileReader, text, fontName, fontSizePx, position, color)
        {
        }

        public float Rotation { get; set; }
        public RotateTextMode RotateMode { get; set; }

        protected override void RenderString(Graphics g, Font font, SolidBrush brush, PointF position, Vertex move)
        {
            var container = g.BeginContainer();

            if (RotateMode == RotateTextMode.RotateBegin)
            {
                move.X *= 2;
            }
            if (RotateMode == RotateTextMode.RotateEnd)
            {
                move.X = 0;
            }


            g.TranslateTransform(position.X, position.Y);
            g.RotateTransform(Rotation);
            g.TranslateTransform(-move.X, -move.Y);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(Text, font, brush, 0, 0);

            g.EndContainer(container);
        }
    }
}
