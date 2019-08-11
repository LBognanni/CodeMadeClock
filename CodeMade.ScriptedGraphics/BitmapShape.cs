using System.Drawing;
using System.Xml.Serialization;

namespace CodeMade.ScriptedGraphics
{
    public class BitmapShape : RectangleShape
    {
        private string path;

        public string Path
        {
            get => path; 
            set
            {
                path = value;
                Image = new Bitmap(path);
            }
        }

        [XmlIgnore]
        public Bitmap Image { get; set; }

        public override void Render(Graphics g, float scaleFactor)
        {
            RectangleF rect = new RectangleF(Left * scaleFactor, Top * scaleFactor, Width * scaleFactor, Height * scaleFactor);

            g.DrawImage(Image, rect);
        }
    }
}
