using CodeMade.ScriptedGraphics;
using System.Drawing;
using System.IO;

namespace CodeMade.Clock.SkinPacks
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
        

        public string GetString(string fileName) =>
            File.ReadAllText(Resolve(fileName));

        public Image LoadImage(string path) =>
            DrawingUtilities.LoadImage(Resolve(path));

        private string Resolve(string fileName) =>
            Path.Combine(_path, fileName);
    }
}