using CodeMade.ScriptedGraphics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMade.Clock.SkinPacks
{
    class VariableReplacingFileReader : IFileReader
    {
        private readonly IFileReader _inner;
        private readonly IDictionary<string, object> _variables;

        public VariableReplacingFileReader(IFileReader inner, IDictionary<string, object> variables)
        {
            _inner = inner;
            _variables = variables;
        }

        public bool FileExists(string fileName) => 
            _inner.FileExists(fileName);

        public string GetFontFile(string fontFile) =>
            _inner.GetFontFile(fontFile);

        public IFileReader GetPack(string pack) =>
            _inner.GetPack(pack);

        public string GetString(string fileName)
        {
            var regexes = _variables.Select(v => new { Regex = new Regex($"\\$\\b{v.Key}\\b"), Replace = v.Value }).ToList();
            var json = _inner.GetString(fileName);
            foreach(var regex in regexes)
            {
                json = regex.Regex.Replace(json, regex.Replace.ToString());
            }

            return json;
        }

        public Image LoadImage(string path) =>
            _inner.LoadImage(path);

        public bool PackExists(string pack) =>
            _inner.PackExists(pack);
    }
    
}