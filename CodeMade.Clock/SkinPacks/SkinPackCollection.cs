using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPackCollection
    {
        private IFileReader _fileReader;

        public IList<SkinPack> Packs { get; }

        public SkinPackCollection(IFileReader resolver)
        {
            Packs = new List<SkinPack>();
            _fileReader = resolver;
        }

        public void Load()
        {

        }
    }
}
