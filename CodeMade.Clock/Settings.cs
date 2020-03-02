using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    class Settings
    {
        public bool HasSettings { get; private set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public string Skin { get; set; }

        [JsonIgnore]
        private string _fileName { get; set; }

        private Settings(string fileName)
        {
            HasSettings = false;
            _fileName = fileName;
        }

        public static Settings Load(string fileName)
        {
            var settings = new Settings(fileName);
            
            if (File.Exists(fileName))
            {
                JsonConvert.PopulateObject(File.ReadAllText(fileName), settings);
                settings.HasSettings = true;
            }

            return settings; 
        }

        public void Save()
        {
            File.WriteAllText(_fileName, JsonConvert.SerializeObject(this));
        }
    }
}
