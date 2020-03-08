using System.Collections.Generic;

namespace CodeMade.Clock.SkinPacks
{
    public class Skin
    {
        public string FileName { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public ClockCanvas Canvas { get; set; }
    }
}