using System.Drawing;

namespace CodeMade.Clock.Tests.FormTests
{
    public class FakeSettings : ISettings
    {
        public bool HasSettings => true;

        public Point Location { get; set; }
        public string SelectedSkin { get; set; }
        public string SelectedSkinpack { get; set; }
        public Size Size { get; set; } = new Size(100, 100);

        public void Save()
        {
        }
    }
}
