using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class SolidLayer : LayerBase
    {
        private string backgroundColor;
        
        public SolidLayer(string backgroundColor)
        {
            this.backgroundColor = backgroundColor;
        }

        protected override void BeforeTransform(Graphics g)
        {
            base.BeforeTransform(g);
        }
    }
}