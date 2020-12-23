using System;
using System.Drawing;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    public class BitmapShape : RectangleShape
    {
        private readonly IFileReader _fileReader;

        public string Path { get; set; }

        public BitmapShape(IFileReader fileReader)
        {
            Image = new Lazy<Image>(ImageFactory);
            _fileReader = fileReader;
        }

        [XmlIgnore]
        public Lazy<Image> Image { get; set; }

        [XmlIgnore]
        public Image FixedImage { get; set; }

        private Image ImageFactory() => _fileReader.LoadImage(Path);

        public override void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            var ctx = g.BeginContainer();

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.DrawImage(FixedImage ?? Image.Value, rect);

            g.EndContainer(ctx);
        }
    }
}
