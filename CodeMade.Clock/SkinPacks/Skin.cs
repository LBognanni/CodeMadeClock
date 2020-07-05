using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;

namespace CodeMade.Clock.SkinPacks
{
    public class Skin
    {
        public Skin(string definition, string name, string description)
        {
            Name = name;
            Definition = definition;
            Description = description;
            Variables = new Dictionary<string, object>();
        }

        public string Name { get; }
        public string Definition { get; set; }
        public string Description { get; }
        public Dictionary<string, object> Variables { get; }
        public Canvas Canvas { get; private set; }

        internal void Load(IFileReader fileReader)
        {
            if(!string.IsNullOrEmpty(Definition))
            {
                if (fileReader.FileExists(Definition))
                {
                    Canvas = Canvas.Load(Definition, fileReader);
                }
            }
        }
    }
}