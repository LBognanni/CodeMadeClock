using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public virtual Bitmap Render(float scaleFactor = 1)
        {
            int scaleWidth = (int)((float)Width * scaleFactor);
            int scaleHeight = (int)((float)Height * scaleFactor);

            Bitmap bmp = new Bitmap(scaleWidth, scaleHeight);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
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
            return JsonConvert.DeserializeObject<Canvas>(json, GetSerializerSettings());
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
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, GetSerializerSettings());
            File.WriteAllText(fileName, json);
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesBinder()
            };
        }

        class KnownTypesBinder : ISerializationBinder
        {
            private static Type[] _types = typeof(Canvas).Assembly.GetTypes();
            private static DefaultSerializationBinder _binder = new DefaultSerializationBinder();

            public Type BindToType(string assemblyName, string typeName)
            {
                return _types.SingleOrDefault(t => t.Name == typeName) ?? _binder.BindToType(assemblyName, typeName);
            }

            public void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                if (_types.Any(t => t.FullName == serializedType.FullName))
                {
                    assemblyName = null;
                    typeName = serializedType.Name;
                }
                else
                {
                    _binder.BindToName(serializedType, out assemblyName, out typeName);
                }
            }
        }
    }
}