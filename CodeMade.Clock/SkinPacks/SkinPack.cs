﻿using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPack
    {
        private readonly IFileReader _fileReader;

        public List<Skin> Skins { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Version Version { get; set; }

        public SkinPack(IFileReader fileReader)
        {
            _fileReader = fileReader;
            Skins = new List<Skin>();
        }

        public static SkinPack Load(string path) => Load(new FileReader(path));
       
        public static SkinPack Load(IFileReader fileReader)
        {
            const string skinPackFileName = "skinpack.json";

            if(!fileReader.FileExists(skinPackFileName))
            {
                throw new EntryPointNotFoundException("File skinpack.json is missing in container.");
            }

            var skinPack = new SkinPack(fileReader);
            JsonConvert.PopulateObject(fileReader.GetString(skinPackFileName), skinPack);

            skinPack.LoadSkins(fileReader);

            skinPack.Verify();

            return skinPack;
        }

        private void LoadSkins(IFileReader fileReader)
        {
            foreach(var skin in this.Skins)
            {
                skin.Load(fileReader);
            }
        }

        internal void Verify()
        {
            foreach(var skin in Skins)
            {
                if(skin.Canvas == null)
                {
                    throw new ValidationFailedException($"Could not load skin file {skin.Definition} for {skin.Name}");
                }
            }
        }

        public class ValidationFailedException : Exception
        {
            public ValidationFailedException(string message) : base(message)
            {
            }
        }
    }
}
