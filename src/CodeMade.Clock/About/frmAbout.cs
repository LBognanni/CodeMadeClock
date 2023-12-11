using CodeMade.GithubUpdateChecker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Diagnostics;

namespace CodeMade.Clock
{
    public partial class frmAbout : Form, IViewFor<AboutViewModel>
    {
        public AboutViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as AboutViewModel; }

        public frmAbout(IVersionGetter versionGetter)
        {
            InitializeComponent();
            lblNewVersion.Text = "";
            lblNewVersion.Links.Add(0, 0, "https://codemade.net");

            ViewModel = new AboutViewModel(versionGetter, () => Assembly.GetExecutingAssembly().GetName().Version);
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.CurrentVersionMessage, frm => frm.lblCurrentVersion.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, vm => vm.NewVersionMessage, frm => frm.lblNewVersion.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, vm => vm.NewVersionLink, frm => frm.lblNewVersion.Tag)
                    .DisposeWith(disposable);
            });
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenLink(object sender, EventArgs e)
        {
            if (sender is Control ctl)
            {
                string url = ctl.Tag?.ToString();
                if (!string.IsNullOrWhiteSpace(url))
                {
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(url);
                        psi.UseShellExecute = true;
                        System.Diagnostics.Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening link: {ex.Message}.\r\n Visit {url}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OpenLinkLabelLink(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink(sender, EventArgs.Empty);
        }
    }
}
