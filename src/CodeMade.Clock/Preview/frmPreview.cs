using CodeMade.ScriptedGraphics;
using NodaTime;
using NodaTime.Extensions;
using ReactiveUI;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Forms;
using ReactiveUI.Winforms;

namespace CodeMade.Clock
{
    public partial class frmPreview : Form, IViewFor<PreviewModel>
    {

        public PreviewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (PreviewModel)value; }

        public frmPreview(string fileToWatch, Type[] knownTypes)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            ViewModel = new PreviewModel(fileToWatch, pbCanvas.Width, pbCanvas.Height, knownTypes);

            this.WhenActivated(disposable =>
            {
                cmbFiles.DataSource = ViewModel.Files;

                var selectionChanged = this.cmbFiles.Events().SelectedIndexChanged;
                this.Bind(ViewModel,
                    vm => vm.FileToWatch,
                    x => x.cmbFiles.SelectedItem,
                    selectionChanged)
                    .DisposeWith(disposable);


                this.Bind(ViewModel,
                        vm => vm.SpecificTimeEnabled,
                        frm => frm.cbSpecificTime.Checked,
                        cbSpecificTime.Events().CheckedChanged)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                        vm => vm.OptimizePreview,
                        frm => frm.cbOptimize.Checked,
                        cbOptimize.Events().CheckedChanged)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.SpecificTimeEnabled,
                    frm => frm.dpTime.Enabled)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.SpecificTime,
                    frm => frm.dpTime.Value,
                    dpTime.Events().ValueChanged)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Image,
                    frm => frm.pbCanvas.Image)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Log,
                    frm => frm.txtLog.Text)
                    .DisposeWith(disposable);


                var resizeEvent = pbCanvas.Events().SizeChanged.Select(_ => pbCanvas.Size).DistinctUntilChanged();
                this.Bind(ViewModel,
                    vm => vm.RenderWidth,
                    frm => frm.pbCanvas.Width,
                    resizeEvent)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    vm => vm.RenderHeight,
                    frm => frm.pbCanvas.Height,
                    resizeEvent)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel,
                    x => x.UpdateImageCommand,
                    f => f.pbCanvas,
                    pbCanvas.Events().Click)
                .DisposeWith(disposable);

                cmbBackground.DataSource = Enum.GetNames(typeof(PreviewModel.BackgroundStyles));
                this.Bind(ViewModel,
                    x => x.BackgroundStyle,
                    f => f.cmbBackground.SelectedIndex,
                    cmbBackground.Events().SelectedIndexChanged,
                    viewToVmConverter: x => (PreviewModel.BackgroundStyles)x,
                    vmToViewConverter: x => (int)x);

                this.OneWayBind(ViewModel,
                    x => x.BackgroundImage,
                    f => f.pbCanvas.BackgroundImage)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    x => x.BackgroundColor,
                    f => f.pbCanvas.BackColor)
                    .DisposeWith(disposable);

            });

        }


        private void CmdCopy_Click(object sender, System.EventArgs e)
        {
            Clipboard.Clear();
            DataObject data = new DataObject();
            using (var ms = new MemoryStream())
            {
                pbCanvas.Image.Save(ms, ImageFormat.Png);
                data.SetData("PNG", false, ms);
                Clipboard.SetDataObject(data, true);
            }
        }

        private void cmdSavePreview_Click(object sender, EventArgs e)
        {
            string fileName = Path.ChangeExtension(ViewModel.FileToWatch, "png");
            pbCanvas.Image.Save(fileName);
        }
    }
}
