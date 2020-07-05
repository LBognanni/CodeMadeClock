using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    class CustomFontTextShape : TextShape, IDisposable
    {
        private readonly PrivateFontCollection _fontCollection;
        private readonly IFileReader _fileReader;
        public string FontFile { get; set; }

        public CustomFontTextShape(IFileReader fileReader)
        {
            _fontCollection = new PrivateFontCollection();
            _fileReader = fileReader;
        }

        protected override Font GetFont(float scaleFactor)
        {
            if (_fontCollection.Families.Length == 0)
            {
                _fontCollection.AddFontFile(_fileReader.GetFontFile(FontFile));
            }

            return new Font(_fontCollection.Families.First(), FontSize * scaleFactor);
        }

        public void Dispose()
        {
            _fontCollection.Dispose();
        }
    }
}
