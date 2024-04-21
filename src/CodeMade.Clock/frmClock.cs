using CodeMade.Clock.LocationMoving;
using CodeMade.Clock.SkinPacks;
using CodeMade.GithubUpdateChecker;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmClock : Form, IScreens, IViewFor<frmClockViewModel>
    {
        private readonly ISettings _settings;
        private readonly SkinPackCollection _skinpacks;
        private Type[] _knownTypes;

        public frmClock(ISettings settings, SkinPackCollection skinpacks, string skinOverride = null, params Type[] knownTypes)
        {
            _settings = settings;
            _skinpacks = skinpacks;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Text = "CodeMade Clock";
            TopMost = true;
            tsmClose.Image = il24.Images[0];
            ShowInTaskbar = false;
            _knownTypes = knownTypes;

            ViewModel = new frmClockViewModel(settings, skinpacks, new ClockTimer(), new LocationFixer(this), skinOverride, knownTypes: knownTypes);

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel,
                    vm => vm.Width,
                    frm => frm.Width,
                    this.Events().Resize)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    vm => vm.Height,
                    frm => frm.Height,
                    this.Events().Resize)
                    .DisposeWith(disposable);

                this.Bind(ViewModel,
                    vm => vm.Location,
                    frm => frm.Location,
                    this.Events().LocationChanged)
                    .DisposeWith(disposable);

                ViewModel.WhenAnyValue(x => x.Image)
                    .WhereNotNull()
                    .Subscribe(UpdateBackground);

            });
        }

        private void UpdateBackground(Bitmap bitmap)
        {
            WinAPI.SetFormBackground(this, bitmap);
        }

        protected override void OnLoad(EventArgs e)
        {
            ControlBox = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            TopMost = true;
            base.OnLoad(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, new UIntPtr(WinAPI.HTCAPTION), IntPtr.Zero);
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(this, e.Location);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                return WinAPI.CreateParams(base.CreateParams);
            }
        }

        public IEnumerable<Rectangle> Screens => Screen.AllScreens.Select(s => s.Bounds);
        public frmClockViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as frmClockViewModel; }

        protected override void OnClick(EventArgs e)
        {
            ViewModel.ForceUpdateImage();
        }


        private void TsmClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SmallerToolStripMenuItem_Click(object sender, EventArgs e) => ViewModel.Smaller();

        private void LargerToolStripMenuItem_Click(object sender, EventArgs e) => ViewModel.Bigger();


        private void tsmSettings_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings(_skinpacks, _settings);
            form.TopMost = this.TopMost;
            if (form.ShowDialog() == DialogResult.OK)
            {
                ViewModel.LoadSkin(null, _knownTypes);
                _settings.Save();
            }
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            frmAbout form = new frmAbout(Program.VersionGetter);
            form.TopMost = this.TopMost;
            form.ShowDialog();
        }

        private void nfiApp_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                BringToFront();
            }
        }
    }
}
