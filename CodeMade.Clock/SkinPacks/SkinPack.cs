using System;
using System.Collections.Generic;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPack
    {
        private string _fileName;

        public List<Skin> Skins { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Version Version { get; set; }

        private SkinPack(string fileName)
        {
            _fileName = fileName;
            Skins = new List<Skin>();
        }

        public static SkinPack Load(string fileName)
        {
            var skinPack = new SkinPack(fileName);

            return skinPack;
        }
    }
}
