using CodeMade.Clock.Controls;
using CodeMade.Clock.SkinPacks;
using ReactiveUI;
using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock
{

    public partial class frmSettings : Form, IViewFor<SettingsViewModel>
    {

        public SettingsViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as SettingsViewModel; }

        public frmSettings(SkinPackCollection skinPacks, ISettings settings)
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel(settings, skinPacks);

            this.WhenActivated(disposable =>
            {
                cmbSkinPack.DataSource = ViewModel.SkinPackList.ToList();
                cmbSkinPack.DropDownStyle = ComboBoxStyle.DropDownList;

                this.Bind(ViewModel,
                    vm => vm.SelectedSkinPack,
                    frm => frm.cmbSkinPack.SelectedItem,
                    cmbSkinPack.Events().SelectedIndexChanged)
                .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Skins,
                    frm => frm.flpSkins.Controls,
                    vmToViewConverterOverride: new SelectListItemConverter())
                    .DisposeWith(disposable);

                cmdSave.Click += (s, a) =>
                {
                    ViewModel.SaveChanges();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };
                cmdCancel.Click += (s, a) =>
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                };
            });
        }

        private void cmdAddSkinPack_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Title = "Load Skinpack", Filter = "Skinpack file (*.skinpack)|*.skinpack" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //_skinPacks.Import(new CombinedFileReader(Path.GetDirectoryName(dialog.FileName)), Path.GetFileName(dialog.FileName));
                        //LoadSkinpackList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
