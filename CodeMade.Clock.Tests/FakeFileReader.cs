using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CodeMade.Clock.Tests
{
    class FakeFileReader : IFileReader
        {
            private readonly IDictionary<string, string> _files;

            public FakeFileReader(IDictionary<string, string> files)
            {
                _files = files;
            }

            public bool FileExists(string fileName) =>
                _files.ContainsKey(fileName);

            public string GetFontFile(string fontFile) => 
                throw new NotImplementedException();

            public IFileReader GetPack(string pack) =>
                this;

            public string GetString(string fileName) =>
                _files[fileName];

            public Image LoadImage(string path) => 
                throw new NotImplementedException();

            public bool PackExists(string pack) => 
                true;
        }
    
}
