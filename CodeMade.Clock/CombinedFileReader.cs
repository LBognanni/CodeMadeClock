using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public class CombinedFileReader : IFileReader
    {
        private FileReader _inner;

        public CombinedFileReader(string path)
        {
            _inner = new FileReader(path);
        }

        public bool FileExists(string fileName) => 
            _inner.FileExists(fileName);

        public string GetFontFile(string fontFile) => 
            _inner.GetFontFile(fontFile);

        public IFileReader GetPack(string pack)
        {
            if (pack.EndsWith(".skinpack"))
            {
                return new ZipFileReader(_inner.Resolve(pack));
            }
            return _inner.GetPack(pack);
        }

        public string GetString(string fileName) => 
            _inner.GetString(fileName);

        public Image LoadImage(string path) =>
            _inner.LoadImage(path);

        public bool PackExists(string pack)
        {
            if (pack.EndsWith(".skinpack"))
            {
                return _inner.FileExists(pack);
            }
            return _inner.PackExists(pack);
        }
    }
}
