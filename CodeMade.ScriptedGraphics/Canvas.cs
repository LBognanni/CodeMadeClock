using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    public class Canvas
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<LayerBase> Layers { get; set; }

        public Canvas(int width, int height, string backgroundColor)
        {
            Width = width;
            Height = height;
            Layers = new List<LayerBase>(new LayerBase[] { new SolidLayer(backgroundColor) });
        }

        public Image Render()
        {
            Bitmap bmp = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(bmp))
            {
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