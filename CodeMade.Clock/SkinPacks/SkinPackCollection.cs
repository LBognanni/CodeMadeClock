using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPackCollection
    {
        private IFileReader _fileReader;

        public IReadOnlyList<SkinPack> Packs { get; }

        public SkinPackCollection(IFileReader fileReader)
        {
            var packs = new List<SkinPack>();
            Packs = packs;
            _fileReader = fileReader;
            var skinPacks = JsonConvert.DeserializeObject<string[]>(_fileReader.GetString("skinpacks.json"));

            foreach(var pack in skinPacks)
            {
                if(_fileReader.PackExists(pack))
                {
                    packs.Add(SkinPack.Load(_fileReader.GetPack(pack)));
                }
            }
        }

    }
}
