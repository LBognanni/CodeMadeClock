using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    public class Layer : IShape
    {
        public List<IShape> Shapes { get; }

        public float TransformRotate { get; set; }
        public Vertex Offset { get; set; }

        public Layer()
        {
            Shapes = new List<IShape>();
            TransformRotate = 0;
            Offset = new Vertex(0, 0);
        }

        public virtual void Render(Graphics g)
        {
            BeforeTransform(g);

            ApplyTransform(g);

            BeforeRenderShapes(g);

            RenderShapes(g);

            AfterRenderShapes(g);

            ResetTransform(g);

            AfterResetTransform(g);
        }

        protected virtual void ResetTransform(Graphics g)
        {
            g.ResetTransform();
        }

        protected virtual void ApplyTransform(Graphics g)
        {
            g.TranslateTransform(Offset.X, Offset.Y);
            g.RotateTransform(TransformRotate, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }

        protected virtual void BeforeTransform(Graphics g)
        {

        }

        protected virtual void AfterRenderShapes(Graphics g)
        {
        }

        protected virtual void RenderShapes(Graphics g)
        {
            foreach(var shape in Shapes)
            {
                shape.Render(g);
            }
        }

        protected virtual void BeforeRenderShapes(Graphics g)
        {
        }

        protected virtual void AfterResetTransform(Graphics g)
        {

        }
    }
}