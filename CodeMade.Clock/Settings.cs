using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace CodeMade.Clock
{
    public class Settings : ISettings
    {
        public bool HasSettings { get; private set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public string SelectedSkinpack { get; set; }
        public string SelectedSkin { get; set; }

        [JsonIgnore]
        private string _fileName { get; set; }

        private Settings(string fileName)
        {
            HasSettings = false;
            SelectedSkin = "blue.json";
            SelectedSkinpack = "default";
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
