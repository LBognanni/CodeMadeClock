using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            new FakeFileReader(_files
                .Where(f => f.Key.StartsWith(pack) && f.Key != pack)
                .Select(f => (f.Key.Substring(pack.Length + 1), f.Value))
                .ToDictionary(el => el.Item1, el => el.Value));

        public string GetString(string fileName) =>
            _files[fileName];

        public Image LoadImage(string path) =>
            throw new NotImplementedException();

        public bool PackExists(string pack) =>
            true;
    }
}