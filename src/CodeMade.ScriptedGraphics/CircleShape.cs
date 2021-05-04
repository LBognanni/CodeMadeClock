﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// Represents a circle with position, radius and color
    /// </summary>
    /// <example>
    /// {
    ///     "$type": "CircleShape",
    ///     "Position": {
    ///         "X": 49,
    ///         "Y": 49
    ///     },
    ///     "Radius": 43,
    ///     "Color": "30-#59fb-#358b"
    /// }
    /// </example>
    public class CircleShape : IShape
    {
        public CircleShape()
        {
        }

        public CircleShape(float x, float y, float radius, string color)
        {
            Position = new Vertex(x, y);
            Radius = radius;
            Color = color;
        }

        /// <summary>
        /// Represents the center of the circle
        /// </summary>
        /// <see cref="Vertex"/>
        public Vertex Position { get; set; }
        
        /// <summary>
        /// Circle radius
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Circle color. Can be a solid color or a gradient.
        /// </summary>
        /// <see cref="CodeMade.ScriptedGraphics.Colors" />
        public string Color { get; set; }

        public virtual void Render(Graphics g, float scaleFactor = 1)
        {
            var diameter = Radius * 2 * scaleFactor;
            var left = (Position.X - Radius) * scaleFactor;
            var top = (Position.Y - Radius) * scaleFactor;

            RenderCircle(g, diameter, left, top);
        }

        protected virtual void RenderCircle(Graphics g, float diameter, float left, float top)
        {
            using (var brush = Color.ParseBrush(new RectangleF(left, top, diameter, diameter)))
            {
                var state = g.BeginContainer();

                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.FillEllipse(brush, left, top, diameter, diameter);

                g.EndContainer(state);
            }
        }
    }
}
