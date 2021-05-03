using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace CodeMade.ScriptedGraphics
{
    /// <summary>
    /// This is the starting block of any rendered image. It contains one or more layers, which in turn contain various shapes
    /// that make up the final image.
    /// 
    /// Most importantly, it defines the Width and Height properties, which represent the Canvas Units that are used everywhere else. 
    /// Canvas Units don't necessarily match with pixels because, being skins can be rendered at any size.
    /// 
    /// There is no need to specify $type because Canvas is only to be used as the root element.
    /// </summary>
    /// <example>
    /// {
    ///     "Width": 101,
    ///     "Height": 101,
    ///     "Layers": [
    ///        // ... layers ...
    ///     ]
    /// }
    /// </example>
    public class Canvas
    {
        /// <summary>
        /// Suggested output width.
        /// Should be an integer value greater than zero.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Suggested output height
        /// Should be an integer value greater than zero.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// A collection of layers that make up the image
        /// </summary>
        /// <see cref="Layer" />
        /// <see cref="SolidLayer"/>
        /// <see cref="GaussianBlurLayer"/>
        /// <see cref="RotateRepeatLayer"/>
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

        public Image RenderAt(int maxWidth, int maxHeight)
        {
            var scaleFactor = Math.Min((float)maxWidth / (float)Width, (float)maxHeight / (float)Height);
            return Render(scaleFactor);
        }

        public virtual Bitmap Render(float scaleFactor = 1)
        {
            int scaleWidth = (int)((float)Width * scaleFactor);
            int scaleHeight = (int)((float)Height * scaleFactor);

            return RenderAt(scaleFactor, scaleWidth, scaleHeight);
        }

        private Bitmap RenderAt(float scaleFactor, int scaleWidth, int scaleHeight)
        {
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

        public static Canvas Load(string fileName, params Type[] knownTypes) =>
            Load(fileName, new FileReader(Path.GetDirectoryName(fileName)), knownTypes);

        public static Canvas Load(string fileName, IFileReader fileReader, params Type[] knownTypes)
        {
            string json = "";
            TryAgain<IOException>(() => json = fileReader.GetString(fileName));

            var canvas = JsonConvert.DeserializeObject<Canvas>(json, GetSerializerSettings(fileReader, knownTypes));

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
            string json = JsonConvert.SerializeObject(this, Formatting.Indented, GetSerializerSettings(new FileReader(Path.GetDirectoryName(fileName))));
            File.WriteAllText(fileName, json);
        }

        internal static JsonSerializerSettings GetSerializerSettings(IFileReader fileReader, params Type[]knownTypes)
        {
            return new JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesBinder(knownTypes),
                ContractResolver = new FileReaderContractResolver(fileReader)
            };
        }
    }

}