using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPackCollection
    {
        private const string SkinpacksIndex = "skinpacks.json";
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public IDictionary<string, SkinPack> Packs { get; }

        internal SkinPackCollection(IFileReader fileReader, IFileWriter fileWriter)
        {
            Packs = new Dictionary<string, SkinPack>();
            _fileReader = fileReader;
            _fileWriter = fileWriter;
            var skinPacks = JsonConvert.DeserializeObject<string[]>(_fileReader.GetString(SkinpacksIndex));

            foreach(var pack in skinPacks)
            {
                if(_fileReader.PackExists(pack))
                {
                    Packs.Add(pack, SkinPack.Load(_fileReader.GetPack(pack)));
                }
            }
        }

        public static SkinPackCollection Load(string localUserSkinPacksFolder, string fallbackPath)
        {
            var writer = new FileWriter(localUserSkinPacksFolder);
            if(!File.Exists(Path.Combine(localUserSkinPacksFolder, SkinpacksIndex)))
            {
                FileWriter.CopyFolder(fallbackPath, Path.Combine(localUserSkinPacksFolder, "default"));
                writer.Write(SkinpacksIndex, "[\"default\"]");
            }

            return new SkinPackCollection(new CombinedFileReader(localUserSkinPacksFolder), writer);
        }

        internal void Import(IFileReader fileReader, string packName)
        {
            if(!fileReader.FileExists(packName))
            {
                throw new FileNotFoundException($"File {packName} does not exist.");
            }

            var packFileReader = fileReader.GetPack(packName);
            var pack = SkinPack.Load(packFileReader);
            if(Packs.ContainsKey(pack.Name))
            {
                throw new DuplicatePackException($"A skin pack named {pack.Name} has already been imported.");
            }

            _fileWriter.Import(fileReader.Resolve(packName));
            Packs.Add(pack.Name, pack);
            _fileWriter.Write(SkinpacksIndex, JsonConvert.SerializeObject(Packs.Keys));
        }

        public class DuplicatePackException : Exception 
        {
            public DuplicatePackException(string message): base(message) { }
        }
    }
}
