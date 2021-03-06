﻿using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CodeMade.Clock.SkinPacks
{
    public class SkinPack
    {
        private readonly Type[] _knownTypes;


        public IReadOnlyList<Skin> Skins { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Version Version { get; set; }

        internal SkinPack(params Type[] knownTypes)
        {
            _knownTypes = knownTypes;
            Skins = new List<Skin>();
        }

        public static SkinPack Load(string path, params Type[] knownTypes) => Load(new FileReader(path), knownTypes);
       
        public static SkinPack Load(IFileReader fileReader, params Type[] knownTypes)
        {
            const string skinPackFileName = "skinpack.json";

            if(!fileReader.FileExists(skinPackFileName))
            {
                throw new EntryPointNotFoundException("File skinpack.json is missing in container.");
            }

            var skinPack = new SkinPack(knownTypes);
            JsonConvert.PopulateObject(fileReader.GetString(skinPackFileName), skinPack);

            skinPack.LoadSkins(fileReader);

            skinPack.Verify();

            return skinPack;
        }

        private void LoadSkins(IFileReader fileReader)
        {
            foreach(var skin in Skins)
            {
                skin.Load(fileReader, _knownTypes);
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
