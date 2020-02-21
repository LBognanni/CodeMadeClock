using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

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
            Layer bgLayer;
            if (string.IsNullOrEmpty(backgroundColor))
            {
                bgLayer = new Layer();
            }
            else
            {
                bgLayer = new SolidLayer(backgroundColor);
            }
            Layers = new List<Layer>(new Layer[] { bgLayer });
        }

        public virtual Bitmap Render(float scaleFactor = 1)
        {
            int scaleWidth = (int)((float)Width * scaleFactor);
            int scaleHeight = (int)((float)Height * scaleFactor);

            Bitmap bmp = new Bitmap(scaleWidth, scaleHeight);
            using (var g = Graphics.FromImage(bmp))
            {
                foreach (var layer in Layers)
                {
                    layer.Render(g, scaleFactor);
                }
            }
            return bmp;
        }

        public void Add(IShape shape)
        {
            Layers.Last().Shapes.Add(shape);
        }

        public static Canvas Load(string fileName)
        {
            string json = "";
            TryAgain<IOException>(() => json = File.ReadAllText(fileName));
            string baseFolder = Path.GetDirectoryName(fileName);

            var canvas = JsonConvert.DeserializeObject<Canvas>(json, GetSerializerSettings(new PathResolver(Path.GetDirectoryName(fileName))));

            return canvas;
        }

        private static void TryAgain<TException>(Func<string> fn) where TException : Exception
        {
            for (int nTries = 1; ; ++nTries)
            {
                try
                {
                    fn();
                }
                catch (TException)
                {
                    if (nTries < 5)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    throw;
                }
                break;
            }
        }

        public void Save(string fileName)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, GetSerializerSettings(new PathResolver(Path.GetDirectoryName(fileName))));
            File.WriteAllText(fileName, json);
        }

        internal static JsonSerializerSettings GetSerializerSettings(IPathResolver resolver)
        {
            return new JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesBinder(),
                ContractResolver = new PathContractResolver(resolver)
            };
        }
    }

}