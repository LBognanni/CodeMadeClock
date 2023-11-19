using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CodeMade.ScriptedGraphics.Colors
{
    public record ColoredRegion(Brush Color, Region Region) : IDisposable
    {
        public void Dispose()
        {
            Color?.Dispose();
            Region?.Dispose();
        }
    }

    public class BrushOrColoredRegion : IDisposable
    {
        enum Type
        {
            Brush,
            ColoredRegion
        }

        private Type _type;

        public BrushOrColoredRegion(Brush brush)
        {
            _brush = brush;
            _type = Type.Brush;
        }

        public BrushOrColoredRegion(Brush color, Region region)
        {
            _coloredRegion = new ColoredRegion(color, region);
            _type = Type.ColoredRegion;
        }

        private Brush _brush;
        private ColoredRegion _coloredRegion;

        public bool IsBrush => _type == Type.Brush;


        public void Match(Action<Brush> brush, Action<ColoredRegion> coloredRegion)
        {
            switch (_type)
            {
                case Type.Brush:
                    brush(_brush);
                    break;
                case Type.ColoredRegion:
                    coloredRegion(_coloredRegion);
                    break;
            }
        }

        internal Brush DangerouslyGetBrush() => _brush ?? throw new NullReferenceException();

        public void Dispose()
        {
            Match(brush => brush?.Dispose(), reg => reg?.Dispose());
        }
    }
}
