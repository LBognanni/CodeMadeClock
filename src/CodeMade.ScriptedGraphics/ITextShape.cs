using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public interface ITextShape
    {
        bool Centered { get; set; }
        string Color { get; set; }
        string Font { get; set; }
        string FontFile { get; set; }
        string FontName { get; set; }
        float FontSize { get; set; }
        float FontSizePx { get; set; }
        Vertex Position { get; set; }
        string Text { get; set; }

        void Dispose();
        void Render(Graphics g, float scaleFactor);
    }
}