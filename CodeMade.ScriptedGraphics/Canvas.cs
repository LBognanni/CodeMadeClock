using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    public class Canvas
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<Layer> Layers { get; set; }

        public Canvas(int width, int height, string backgroundColor)
        {
            Width = width;
            Height = height;
            Layers = new List<Layer>(new Layer[] { new SolidLayer(backgroundColor) });
        }

        public Bitmap Render()
        {
            Bitmap bmp = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                foreach(var layer in Layers)
                {
                    layer.Render(g);
                }
            }
            return bmp;
        }

        public void Add(IShape shape)
        {
            Layers.Last().Shapes.Add(shape);
        }
    }
}