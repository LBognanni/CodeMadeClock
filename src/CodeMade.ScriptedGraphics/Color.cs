using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// ## 🎨 Solid colors
    /// 
    /// Colors in the serialization format are inspired by HTML and CSS, and can be represented in one of the following formats:
    /// 
    ///  - `#rrggbbaa`: Each part of the color is encoded by a 2 digits hex value between 00 and FF. The alpha component is optional. For example, fully opaque red is `#ff0000ff` or `#ff0000`. There are a number of online color pickers to help you find the perfect color! [Here is one](https://htmlcolors.com/color-picker)
    ///  - `#rgba` Same as the above but each digit will be automatically doubled. So `#fed3` will be converted to `#ffeedd33`. Again, red will be `#f00f` or `#f00`
    ///  - The name of any [known color](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=net-5.0), like `red` or `blue`
    ///  
    /// ## Linear gradients
    /// 
    /// It's also possible to specify linear gradients by using the format `angle-color1-color2` where `angle` is the angle in degrees, and `color1` and `color2` are the colors in any of the formats above.
    /// For example a 30 degrees gray gradient could be `30-#ccc-gray`
    /// </summary>
    internal static class Colors
    {
        public static Brush ParseBrush(this string s, RectangleF rect)
        {
            // conic gradient: something like c-(.5,.5)-#fff-#000
            if(s.StartsWith("c-"))
            {
                return ParseConicGradient(s, rect);
            }

            // linear gradient brush: something like #fff-#000 or 30-#fff-000
            if(s.Contains("-"))
            {
                return ParseLinearGradient(s, rect);
            }

            return new SolidBrush(s.ToColor());
        }

        private static Brush ParseLinearGradient(string s, RectangleF rect)
        {
            Color color1, color2;
            float angle;

            string[] parts = s.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
                throw new FormatException($"'{s}' is not a valid gradient.");
            }

            return new LinearGradientBrush(rect, color1, color2, angle);
        }

        private static Brush ParseConicGradient(string s, RectangleF rect)
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

            return ConicGradient.Create(rect, colors.ToArray(), cx, cy, angle);
        }


        public static Color ToColor(this string s)
        {
            if(s.StartsWith("#"))
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
