using CodeMade.Clock.SkinPacks;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmSettings : Form
    {
        private readonly SkinPackCollection _skinPacks;
        private readonly ISettings _settings;

        public frmSettings(SkinPackCollection skinPacks, ISettings settings)
        {
            InitializeComponent();
            _skinPacks = skinPacks;
            _settings = settings;

            LoadSkinpackList();
            LoadSelectedSkinpack();
        }

        private void LoadSkinpackList()
        {
            cmbSkinPack.SuspendLayout();
            cmbSkinPack.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSkinPack.DataSource = _skinPacks.Packs;
            cmbSkinPack.DisplayMember = "Name";
            cmbSkinPack.ValueMember = "Name";
            cmbSkinPack.SelectItem(_skinPacks.Packs.FirstOrDefault(p => p.Name == _settings.SelectedSkinpack));
            cmbSkinPack.ResumeLayout();
            cmbSkinPack.SelectedIndexChanged += (s, e) => LoadSelectedSkinpack();
        }

        private void LoadSelectedSkinpack()
        {
            var skinPack = _skinPacks.Packs.First();
            const int icon_size = 128;
            SuspendLayout();

            Skin selectedSkin = skinPack.Skins.FirstOrDefault(s => s.Name == _settings.SelectedSkin);
            slSkins.Bind(skinPack.Skins, selectedSkin, s => s.Name, s => s.Description, s => s.Canvas.RenderAt(icon_size, icon_size));

            ResumeLayout();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            _settings.SelectedSkin = slSkins.GetSelected<Skin>()?.Name;
            Close();
        }
    }
}
