using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMade.ScriptedGraphics.Colors
{
    /// <summary>
    /// 
    /// Clock supports several color styles: solid, linear gradients, conical gradients, and radial gradients
    /// 
    /// ## 🎨 Solid colors
    /// 
    /// Colors in the serialization format are inspired by HTML and CSS, and can be represented in one of the following formats:
    /// 
    ///  - `#rrggbbaa`: Each part of the color is encoded by a 2 digits hex value between 00 and FF. The alpha component is optional. For example, fully opaque red is `#ff0000ff` or `#ff0000`. There are a number of online color pickers to help you find the perfect color! [Here is one](https://htmlcolors.com/color-picker)
    ///  - `#rgba` Same as the above but each digit will be automatically doubled. So `#fed3` will be converted to `#ffeedd33`. Again, red will be `#f00f` or `#f00`
    ///  - The name of any [known color](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=net-5.0), like `red` or `blue`
    ///  
    /// ## ➡ Linear gradients
    /// 
    /// You can specify linear gradients by using the format `angle-color1-color2[...-colorN]` where `angle` is the angle in degrees, and `color1` and `color2` are the colors in any of the formats above.
    /// Examples of valid linear gradients are:
    ///  - `0-red-blue` will draw a horizontal linear gradient from red to blue
    ///  - `45-red-blue-green` will draw a linear gradient from red to blue to green, angled at 45 degrees
    ///  - `90-#fff-#0008` will draw a vertical linear gradient from solid white to 50% transparent black
    /// 
    /// ## ❇ Radial gradients
    /// 
    /// Radial gradients take the form of `(cx,cy[,sz])-color1-color2[...-colorN]` where `cx` and `cy` are the coordinates of the center of the gradient, sz is the size of the gradient, and `color1..N` are the colors in any of the formats above.
    /// `cx` and `cy` are expressed in fractional values, where (0.5, 0.5)is the center of the rectangle, and (0, 0) is the top left corner.
    /// `sz` is optional and defaults to 1.
    /// 
    /// Examples of valid radial gradients are:
    ///  - `(0.5,0.5)-red-green-blue` will draw a radial gradient from red to green to blue, with the center of the gradient at the center of the rectangle, and a radius of half the size of the shape
    ///  - `(0.5,0)-#fff-#000` will draw a radial gradient from white to black, with the center of the gradient at the center of the top edge of the rectangle, and a radius of half the size of the shape
    ///  - `(0.5,0,2)-#fff-#000` will draw a radial gradient from white to black, with the center of the gradient at the center of the top edge of the rectangle, and a radius of the size of the shape
    /// 
    /// ## 🔄 Conical gradients
    /// 
    /// Conical gradients take the form of `c[-(cx,cy)][-angle]-color1-color2[...-colorN]` where `cx` and `cy` are the coordinates of the center of the gradient, 
    /// `angle` is the starting angle in degrees, and `color1..N` are the colors in any of the formats above.
    /// `cx` and `cy` are optional and default to 0.5, 0.5 (the center of the rectangle). `angle` is also optional and defaults to 0.
    /// 
    /// Note that the first color is not repeated at the end of the gradient, so if you want a smooth transition you should repeat the first color at the end.
    /// 
    /// Examples of valid conical gradients are:
    ///  - `c-red-green-blue` will draw a conical gradient from red to green to blue, starting at 0 degrees, and with the center of the gradient at the center of the rectangle
    ///  - `c-red-green-blue-red` will do the same as above, but will have a smooth transition between blue and red
    ///  - `c-(.75,.25)-#fff-#000` will draw a conical gradient from white to black, starting at 0 degrees, and with the center of the gradient at 75% of the width and 25% of the height of the rectangle
    ///  - `c-(.5,.5)-45-#fff-#000` will draw a conical gradient from white to black, starting at 45 degrees, and with the center of the gradient at the center of the rectangle
    /// </summary>
    public static class Colors
    {
        public static IEnumerable<BrushOrColoredRegion> ParseBrush(this string s, RectangleF rect)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new[] { new BrushOrColoredRegion(new SolidBrush(Color.Transparent)) };
            }

            // conic gradient: something like c-(.5,.5)-#fff-#000
            if (s.StartsWith("c-"))
            {
                return new[] { ParseConicGradient(s, rect) };
            }

            if (s.StartsWith("("))
            {
                return ParseRadialGradient(s, rect);
            }

            // linear gradient brush: something like #fff-#000 or 30-#fff-000
            if (s.Contains("-"))
            {
                return new[] { ParseLinearGradient(s, rect) };
            }

            return new[] { new BrushOrColoredRegion(new SolidBrush(s.ToColor())) };
        }

        private static IEnumerable<BrushOrColoredRegion> ParseRadialGradient(string s, RectangleF rect)
        {
            var matches = Regex.Match(s, @"\(([0-9,\.\-\s]+)\)((?:-[\w#]+)+)");
            if (matches.Groups.Count != 3)
            {
                throw new FormatException($"Invalid radial gradient format: `{s}`");
            }
            var coords = matches.Groups[1].Value.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var x = float.Parse(coords[0]);
            var y = float.Parse(coords[1]);
            var sz = 1.0f;
            if(coords.Length == 3)
            {
                sz = float.Parse(coords[2]);
            }
            var colors = matches.Groups[2].Value.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return RadialGradient.Create(rect, (float)x, (float)y, (float)sz, colors);
        }

        private static BrushOrColoredRegion ParseLinearGradient(string s, RectangleF rect)
        {
            Color color1, color2;
            float angle;

            var parts = s.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                color1 = parts[0].ToColor();
                color2 = parts[1].ToColor();
                angle = 0;
            }
            else if (parts.Length == 3)
            {
                angle = float.Parse(parts[0]);
                color1 = parts[1].ToColor();
                color2 = parts[2].ToColor();
            }
            else
            {
                angle = float.Parse(parts[0]);
                var colors = parts.Skip(1).Select(x => x.ToColor()).ToArray();
                var multiColorBrush = new LinearGradientBrush(rect, Color.DeepPink, Color.DeepPink, angle)
                {
                    InterpolationColors = new ColorBlend()
                    {
                        Positions = colors.Select((c, i) => i / (float)(colors.Length - 1)).ToArray(),
                        Colors = colors
                    }
                };
                return new BrushOrColoredRegion(multiColorBrush);
            }

            var brush = new LinearGradientBrush(rect, color1, color2, angle);
            return new BrushOrColoredRegion(brush);
        }

        private static BrushOrColoredRegion ParseConicGradient(string s, RectangleF rect)
        {
            var colors = new List<Color>();
            var angle = 0.0f;
            var cx = 0.5f;
            var cy = 0.5f;
            var splits = s.Substring(1).Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var split in splits)
            {
                if (split.StartsWith("("))
                {
                    var parts = split.Split(", ()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        cx = float.Parse(parts[0]);
                        cy = float.Parse(parts[1]);
                    }
                    continue;
                }
                if (float.TryParse(split, out var a))
                {
                    angle = a;
                    continue;
                }
                colors.Add(split.ToColor());
            }

            return new BrushOrColoredRegion(ConicGradient.Create(rect, colors.ToArray(), cx, cy, angle));
        }


        public static Color ToColor(this string s)
        {
            if (s.StartsWith("#"))
            {
                return ParseHtmlColor(s.Substring(1));
            }
            else
            {
                return Color.FromName(s);
            }
        }

        public static string ToHtml(this Color c)
        {
            return $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}".ToLower();
        }

        private static Color ParseHtmlColor(string s)
        {
            string hexR, hexG, hexB, hexA = "FF";

            switch (s.Length)
            {
                case 3: // #rgb
                    hexR = new string(new char[] { s[0], s[0] });
                    hexG = new string(new char[] { s[1], s[1] });
                    hexB = new string(new char[] { s[2], s[2] });
                    break;
                case 4: //#rgba
                    hexA = new string(new char[] { s[3], s[3] });
                    goto case 3;
                case 6: // #rrggbb
                    hexR = new string(new char[] { s[0], s[1] });
                    hexG = new string(new char[] { s[2], s[3] });
                    hexB = new string(new char[] { s[4], s[5] });
                    break;
                case 8: //#rrggbbaa
                    hexA = new string(new char[] { s[6], s[7] });
                    goto case 6;
                default:
                    throw new FormatException($"Value `#{s}` is not one of the allowed color formats: #rgb, #rrggbb, #rgba, #rrffbbaa");
            }

            return Color.FromArgb(
                Convert.ToInt32(hexA, 16),
                Convert.ToInt32(hexR, 16),
                Convert.ToInt32(hexG, 16),
                Convert.ToInt32(hexB, 16));
        }
    }
}
