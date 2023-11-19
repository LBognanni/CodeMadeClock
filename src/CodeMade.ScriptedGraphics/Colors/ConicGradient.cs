using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CodeMade.ScriptedGraphics.Colors
{
    internal static class ConicGradient
    {
        private static int MixValue(float factor, int v1, int v2)
        {
            return (int)(v1 * factor) + (int)(v2 * (1 - factor));
        }

        private static Color MixColors(float factor, Color c1, Color c2)
        {
            return Color.FromArgb(
                MixValue(factor, c1.A, c2.A),
                MixValue(factor, c1.R, c2.R),
                MixValue(factor, c1.G, c2.G),
                MixValue(factor, c1.B, c2.B));
        }

        static Color MixColorsArray(IReadOnlyCollection<Color> colors)
        {
            return Color.FromArgb(
                (int)colors.Average(c => c.A),
                (int)colors.Average(c => c.R),
                (int)colors.Average(c => c.G),
                (int)colors.Average(c => c.B)
            );
        }

        public static Brush Create(RectangleF rect, Color[] colors, float cx = 0.5f, float cy = 0.5f, double startAngle = 0)
        {
            var allTheColors = (colors.Length switch
            {
                2 => Enumerable.Range(0, 10).Select(n => MixColors(1.0f - n / 10.0f, colors[0], colors[1])).ToArray(),
                3 => new[]{
                colors[0],
                MixColors(0.5f, colors[0], colors[1]),
                colors[1],
                MixColors(0.5f, colors[1], colors[2]),
                colors[2]
            },
                _ => colors
            }).ToList();

            var sz = Math.Max(rect.Width, rect.Height);
            var radius = sz * (Math.Max(cx, cy) + 1.0);
            startAngle = (float)(startAngle / 180.0 * Math.PI);

            using var pth = CreatePolygonPath(rect.Left + cx * sz, rect.Top + cy * sz, startAngle, allTheColors.Count, radius);
            return new PathGradientBrush(pth)
            {
                SurroundColors = allTheColors.ToArray(),
                CenterColor = MixColorsArray(allTheColors),
                CenterPoint = new PointF(rect.Left + rect.Width * cx, rect.Left + rect.Height * cy),
                Blend = new Blend
                {
                    Factors = new[] { 1.0f, 1.0f },
                    Positions = new[] { 0.0f, 1.0f }
                }
            };
        }

        static GraphicsPath CreatePolygonPath(float cx, float cy, double startAngle, int vertexCount, double radius)
        {
            var pts = vertexCount - 1;
            var polyPoints = new List<PointF>();
            var increment = Math.PI * 2 * (1.0 / pts);
            var angle = startAngle - Math.PI / 2;

            polyPoints.Add(PlotPoint(angle, radius, cx, cy));
            angle -= 0.001;
            for (var i = 0; i < pts; i++)
            {
                angle += increment;
                polyPoints.Add(PlotPoint(angle, radius, cx, cy));
            }

            var path = new GraphicsPath();
            path.AddPolygon(polyPoints.ToArray());
            return path;
        }

        static PointF PlotPoint(double angle, double radius, float cx, float cy) =>
            new PointF(
                    cx + (float)(Math.Cos(angle) * radius),
                    cy + (float)(Math.Sin(angle) * radius)
                );
    }
}
