using System.Drawing;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// A layer whose contents will get drawn rotated at various angles.
    /// 
    /// It's especially useful for repeating hour and minute ticks on a clock face.
    /// 
    /// For example, you could have a rectangle at (-1, -20) with size(2, 10), when setting 
    /// the value of RepeatCount to 12 and the value of RepeatRotate to 30 you will have drawn the hour ticks on a clock.
    /// This is because the rectangle will be drawn 12 times, each time rotated by 30 degrees more than the previous.
    /// </summary>
    /// <example>
    /// {
    ///     "$type": "RotateRepeatLayer",
    ///     "Shapes": [
    ///         {
    ///             "$type": "Shape",
    ///             "Color": "#aefe",
    ///             "Path": "0,-42 1.5,-40 0,-35 -1.5,-40"
    ///         }
    ///     ],
    ///     "Offset": {
    ///         "X": 49,
    ///         "Y": 49
    ///     },
    ///     "RepeatRotate": 30,
    ///     "RepeatCount": 12
    /// }
    /// </example>
    /// <inheritdoc cref="Layer"/>
    public class RotateRepeatLayer : Layer
    {
        public RotateRepeatLayer()
        {
        }

        /// <summary>
        /// Number of times the content will be repeated
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// The angle(deg) at which to repeat the contents
        /// </summary>
        public float RepeatRotate { get; set; }

        /// <summary>
        /// The angle(deg) at which to start repeating the contents 
        /// </summary>
        public float Start { get; set; }

        public override Layer Copy()
            => new RotateRepeatLayer
            {
                Offset = Offset,
                Rotate = Rotate,
                RepeatCount = RepeatCount,
                RepeatRotate = RepeatRotate,
                Start = Start,
            };


        private int _repeatCounter;

        public override void Render(Graphics g, float scaleFactor = 1)
        {
            for (_repeatCounter = 0; _repeatCounter < RepeatCount; _repeatCounter++)
            {
                base.Render(g, scaleFactor);
            }
        }

        protected override void ApplyTransform(Graphics g, float scaleFactor)
        {
            base.ApplyTransform(g, scaleFactor);
            g.RotateTransform(Start + RepeatRotate * (float)_repeatCounter, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }
    }
}
