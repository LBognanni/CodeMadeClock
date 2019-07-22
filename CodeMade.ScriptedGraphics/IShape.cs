using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public interface IShape
    {
        void Render(Graphics g, float scaleFactor);
    }
}