﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeMade.ScriptedGraphics
{
    internal static class ColorExtensions
    {
        public static Brush ParseBrush(this string s, RectangleF rect)
        {
            if(s.Contains("-"))
            {
                Color color1, color2;
                float angle;

                string[] parts = s.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if(parts.Length == 2)
                {
                    color1 = parts[0].ToColor();
                    color2 = parts[1].ToColor();
                    angle = 0;
                }
                else if(parts.Length == 3)
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
            else
            {
                return new SolidBrush(s.ToColor());
            }
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
