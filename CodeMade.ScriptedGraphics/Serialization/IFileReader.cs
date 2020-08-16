using System.Drawing;

namespace CodeMade.ScriptedGraphics 
{
    public interface IFileReader
    {
        bool FileExists(string fileName);
        string GetString(string fileName);
        Image LoadImage(string path);
        string GetFontFile(string fontFile);
        bool PackExists(string pack);
        IFileReader GetPack(string pack);
        string Resolve(string fileName);
    }
}