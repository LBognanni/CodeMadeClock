using System.Drawing;
using System.IO;

namespace CodeMade.ScriptedGraphics
{
    public class FileReader : IFileReader
    {
        private readonly string _path;

        public FileReader(string path) => 
            _path = path;

        public bool FileExists(string fileName) => 
            File.Exists(Resolve(fileName));

        public string GetFontFile(string fontFile) =>
            Resolve(fontFile);

        public IFileReader GetPack(string pack) =>
            new FileReader(Resolve(pack));

        public string GetString(string fileName) =>
            File.ReadAllText(Resolve(fileName));

        public Image LoadImage(string path) =>
            DrawingUtilities.LoadImage(Resolve(path));

        public bool PackExists(string pack) =>
            Directory.Exists(Resolve(pack));

        public string Resolve(string fileName) =>
            Path.Combine(_path, fileName);
    }
}