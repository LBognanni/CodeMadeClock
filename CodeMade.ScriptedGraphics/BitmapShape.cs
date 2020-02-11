using System;
using System.Drawing;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    public class BitmapShape : RectangleShape
    {
        public string Path { get; set; }

        public BitmapShape()
        {
            Image = new Lazy<Image>(ImageFactory);
        }

        [XmlIgnore]
        public Lazy<Image> Image { get; set; }

        [XmlIgnore]
        public Image FixedImage { get; set; }

        private Image ImageFactory() => DrawingUtilities.LoadImage(Path);

        public override void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            g.DrawImage(FixedImage ?? Image.Value, rect);
        }
    }
}
