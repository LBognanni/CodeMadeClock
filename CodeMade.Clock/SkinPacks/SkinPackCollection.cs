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
        private IFileReader _fileReader;

        public IDictionary<string, SkinPack> Packs { get; }

        internal SkinPackCollection(IFileReader fileReader)
        {
            Packs = new Dictionary<string, SkinPack>();
            _fileReader = fileReader;
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
            if(!File.Exists(Path.Combine(localUserSkinPacksFolder, SkinpacksIndex)))
            {
                CopyFolder(fallbackPath, Path.Combine(localUserSkinPacksFolder, "default"));
                File.WriteAllText(Path.Combine(localUserSkinPacksFolder, "skinpacks.json"), "[\"default\"]");
            }

            return new SkinPackCollection(new FileReader(localUserSkinPacksFolder));
        }

        private static void CopyFolder(string sourceFolder, string destinationFolder)
        {
            if(!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            foreach(var directory in Directory.GetDirectories(sourceFolder))
            {
                CopyFolder(directory, Path.Combine(destinationFolder, Path.GetFileName(directory)));
            }

            foreach(var file in Directory.GetFiles(sourceFolder))
            {
                string destinationFile = Path.Combine(destinationFolder, Path.GetFileName(file));
                File.Copy(file, destinationFile);
            }
        }
    }
}
