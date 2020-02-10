using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    public class Layer : IShape
    {
        public List<IShape> Shapes { get; }

        public float Rotate { get; set; }
        public Vertex Offset { get; set; }

        private GraphicsContainer originalTransform;

        public bool ShouldSerializeTransformRotate()
        {
            return Rotate != 0;
        }

        public bool ShouldSerializeOffset()
        {
            return Offset.X != 0 || Offset.Y != 0;
        }

        public Layer()
        {
            Shapes = new List<IShape>();
            Rotate = 0;
            Offset = new Vertex(0, 0);
        }

        public virtual void Render(Graphics g, float scaleFactor = 1)
        {
            BeforeTransform(g, scaleFactor);

            ApplyTransform(g, scaleFactor);

            BeforeRenderShapes(g, scaleFactor);

            RenderShapes(g, scaleFactor);

            AfterRenderShapes(g, scaleFactor);

            ResetTransform(g, scaleFactor);

            AfterResetTransform(g, scaleFactor);
        }

        protected virtual void ResetTransform(Graphics g, float scaleFactor)
        {
            g.ResetTransform();
        }

        protected virtual void ApplyTransform(Graphics g, float scaleFactor)
        {
            g.TranslateTransform(Offset.X * scaleFactor, Offset.Y * scaleFactor);
            g.RotateTransform(Rotate, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }

        protected virtual void BeforeTransform(Graphics g, float scaleFactor)
        {
            originalTransform = g.BeginContainer();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }

        protected virtual void AfterRenderShapes(Graphics g, float scaleFactor)
        {
        }

        protected virtual void RenderShapes(Graphics g, float scaleFactor)
        {
            foreach(var shape in Shapes)
            {
                shape.Render(g, scaleFactor);
            }
        }

        protected virtual void BeforeRenderShapes(Graphics g, float scaleFactor)
        {
        }

        protected virtual void AfterResetTransform(Graphics g, float scaleFactor)
        {
            if (originalTransform != null)
                g.EndContainer(originalTransform);
        }
    }
}