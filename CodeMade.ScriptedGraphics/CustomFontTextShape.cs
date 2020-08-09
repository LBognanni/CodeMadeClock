using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace CodeMade.ScriptedGraphics
{
    class CustomFontTextShape : TextShape, IDisposable
    {
        private readonly PrivateFontCollection _fontCollection;
        private readonly IPathResolver _resolver;
        public string FontFile { get; set; }

        public CustomFontTextShape(IPathResolver resolver)
        {
            _fontCollection = new PrivateFontCollection();
            _resolver = resolver;
        }

        protected override Font GetFont(float scaleFactor)
        {
            if (_fontCollection.Families.Length == 0)
            {
                _fontCollection.AddFontFile(_resolver.Resolve(FontFile));
            }

            return new Font(_fontCollection.Families.First(), FontSize * scaleFactor);
        }

        public void Dispose()
        {
            _fontCollection.Dispose();
        }
    }
}
