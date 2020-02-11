using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics
{
    public class DonutShape : CircleShape
    {
        public float InnerRadius { get; set; }
        private Lazy<DeleteCircleShape> _innerShape;

        public DonutShape()
        {
            _innerShape = new Lazy<DeleteCircleShape>(InnerShapeFactory);
        }

        private DeleteCircleShape InnerShapeFactory()
        {
            return new DeleteCircleShape { Position = this.Position, Radius = InnerRadius };
        }

        public override void Render(Graphics g, float scaleFactor = 1)
        {
            base.Render(g, scaleFactor);
            _innerShape.Value.Render(g, scaleFactor);
        }
    }
}
