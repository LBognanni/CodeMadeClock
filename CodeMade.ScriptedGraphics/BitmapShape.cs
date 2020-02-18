using System;
using System.Drawing;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    public class BitmapShape : RectangleShape
    {
        private readonly IPathResolver _resolver;

        public string Path { get; set; }

        public BitmapShape(IPathResolver resolver)
        {
            Image = new Lazy<Image>(ImageFactory);
            this._resolver = resolver;
        }

        [XmlIgnore]
        public Lazy<Image> Image { get; set; }

        [XmlIgnore]
        public Image FixedImage { get; set; }

        private Image ImageFactory() => DrawingUtilities.LoadImage(_resolver.Resolve(Path));

        public override void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            g.DrawImage(FixedImage ?? Image.Value, rect);
        }
    }
}
