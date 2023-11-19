using CodeMade.ScriptedGraphics.Colors;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CodeMade.ScriptedGraphics
{
    internal static class RadialGradient
    {
        public static IEnumerable<BrushOrColoredRegion> Create(RectangleF rect, float cx, float cy, float sz, string[] colors)
        {
            var centerPoint = new PointF(rect.Left + (float)cx * rect.Width, rect.Top + (float)cy * rect.Height);
            var result = new List<BrushOrColoredRegion>();

            GraphicsPath lastPath = null;

            float lastRadius = 0;

            for (int idx = 1; idx < colors.Length; ++idx)
            {
                var thisSz = sz * ((float)idx / (float)(colors.Length - 1));
                var thisRadius = (Math.Max(rect.Width, rect.Height) / 2) * thisSz;
                var path = CreateCirclePath(rect.Left + cx * rect.Width, rect.Top + cy * rect.Height, thisRadius);

                if (lastPath == null)
                {
                    var brush = new PathGradientBrush(path)
                    {
                        SurroundColors = new[] { colors[idx].ToColor() },
                        CenterColor = colors[idx - 1].ToColor(),
                        CenterPoint = centerPoint
                    };
                    result.Add(new BrushOrColoredRegion(brush));
                }
                else
                {
                    var donut = new Region(path);
                    donut.Exclude(lastPath);
                    var fs = 1.0f - (thisRadius - lastRadius) / thisRadius;
                    var brush = new PathGradientBrush(path)
                    {
                        SurroundColors = new[] { colors[idx].ToColor() },
                        CenterColor = colors[idx - 1].ToColor(),
                        CenterPoint = centerPoint,
                        FocusScales = new PointF(fs, fs)
                    };
                    result.Add(new BrushOrColoredRegion(brush, donut));

                    lastPath.Dispose();
                }

                lastPath = path;
                lastRadius = thisRadius;
            }
            lastPath.Dispose();


            using var pth = CreateCirclePath(rect.Left + cx * rect.Width, rect.Top + cy * rect.Height, (Math.Max(rect.Width, rect.Height) / 2) * sz);
            using var rectPath = new GraphicsPath();
            rectPath.AddRectangle(rect);
            var rgn = new Region(rectPath);
            rgn.Exclude(pth);

            result.Add(new BrushOrColoredRegion(new SolidBrush(colors.Last().ToColor()), rgn));

            return result;
        }

        static GraphicsPath CreateCirclePath(float cx, float cy, float radius)
        {
            var path = new GraphicsPath();
            path.AddEllipse(new RectangleF(cx - radius, cy - radius, radius * 2, radius * 2));
            return path;
        }
    }
}
