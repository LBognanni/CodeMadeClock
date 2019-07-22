﻿using System.Collections.Generic;
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
            g.TranslateTransform(Offset.X, Offset.Y);
            g.RotateTransform(TransformRotate, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }

        protected virtual void BeforeTransform(Graphics g, float scaleFactor)
        {

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

        }
    }
}