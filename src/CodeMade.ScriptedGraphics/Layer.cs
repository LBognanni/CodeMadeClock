using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// Represents the base container of Shapes.
    /// 
    /// Because a Layer is also a Shape, layers can contain other layers.
    /// </summary>
    /// <example>
    /// {
    ///     "$type": "Layer",
    ///     "Rotate": 45.2,
    ///     "Offset": {
    ///         "X": 30,
    ///         "Y": 0
    ///     },
    ///     "Shapes": [
    ///         // ... shapes ...
    ///     ]
    /// }
    /// </example>
    public class Layer : IShape
    {
        /// <summary>
        /// A list of all the shapes contained in this layer
        /// </summary>
        public List<IShape> Shapes { get; }

        /// <summary>
        /// Layer rotation, in degrees
        /// </summary>
        public float Rotate { get; set; }

        /// <summary>
        /// Layer offset in Canvas Units
        /// </summary>
        /// <see cref="CodeMade.ScriptedGraphics.Canvas"/>
        public Vertex Offset { get; set; }

        private GraphicsContainer _originalTransform;

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
            g.RotateTransform(Rotate, MatrixOrder.Prepend);
        }

        protected virtual void BeforeTransform(Graphics g, float scaleFactor)
        {
            _originalTransform = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.HighQuality;
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
            if (_originalTransform != null)
                g.EndContainer(_originalTransform);
        }
    }
}