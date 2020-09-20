using System.Drawing;

namespace CodeMade.Clock
{
    public interface ISettings
    {
        bool HasSettings { get; }
        Point Location { get; set; }
        string SelectedSkin { get; set; }
        string SelectedSkinpack { get; set; }
        Size Size { get; set; }
        void Save();
    }
}