using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeMade.Clock.SkinPacks
{
    public class Skin
    {
        public Skin(string definition, string name, string description)
        {
            Name = name;
            Definition = definition;
            Description = description;
            Variables = new Dictionary<string, object>();
        }

        public string Name { get; }
        public string Definition { get; set; }
        public string Description { get; }
        public Dictionary<string, object> Variables { get; }
        public Canvas Canvas { get; private set; }

        internal void Load(IFileReader fileReader)
        {
            if(!string.IsNullOrEmpty(Definition))
            {
                if (fileReader.FileExists(Definition))
                {
                    Canvas = Canvas.Load(Definition, new VariableReplacingFileReader(fileReader, Variables));
                }
            }
        }

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
        }
    }
}