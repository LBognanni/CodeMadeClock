using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        internal void Import(IFileReader fileReader, string packFileName)
        {
            if(!fileReader.FileExists(packFileName))
            {
                throw new FileNotFoundException($"File {packFileName} does not exist.");
            }

            var packFileReader = fileReader.GetPack(packFileName);
            var pack = SkinPack.Load(packFileReader);

            var installedPack = Packs.FirstOrDefault(p => p.Value.Name == pack.Name);
            if (installedPack.Key != null)
            {
                if (pack.Version > installedPack.Value.Version)
                {
                    Packs.Remove(installedPack.Key);
                    Packs.Add(packFileName, pack);
                    _fileWriter.ReplacePack(installedPack.Key, fileReader.Resolve(packFileName));
                }
                else
                {
                    throw new DuplicatePackException($"A skin pack named {pack.Name} has already been imported.");
                }
            }
            else
            {
                _fileWriter.Import(fileReader.Resolve(packFileName));
                Packs.Add(packFileName, pack);
            }

            _fileWriter.Write(SkinpacksIndex, JsonConvert.SerializeObject(Packs.Keys));
        }

        public class DuplicatePackException : Exception 
        {
            public DuplicatePackException(string message): base(message) { }
        }
    }
}
