using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ReactiveUI.Winforms;
using ReactiveUI;
using System.Reactive.Disposables;

namespace CodeMade.Clock.Controls
{
    public partial class SelectListItem : UserControl, IViewFor<SelectListItemViewModel>
    {
        public event EventHandler SelectedChanged;

        public SelectListItem() : this(new SelectListItemViewModel())
        {
        }

        public SelectListItem(SelectListItemViewModel model)
        {
            ViewModel = model;
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(
                    ViewModel,
                    x => x.CheckImage,
                    f => f.imgChecked.Image
                    ).DisposeWith(disposable);
                this.OneWayBind(
                    ViewModel,
                    x => x.Title,
                    f => f.lblTitle.Text
                    ).DisposeWith(disposable);
                this.OneWayBind(
                    ViewModel,
                    x => x.Description,
                    f => f.lblDescription.Text
                    ).DisposeWith(disposable);
                this.OneWayBind(
                    ViewModel,
                    x => x.Image,
                    f => f.imgItem.Image
                    ).DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.SelectCommand, f => f, this.Events().Click);
                this.BindCommand(ViewModel, x => x.SelectCommand, f => f.imgChecked, imgChecked.Events().Click);
                this.BindCommand(ViewModel, x => x.SelectCommand, f => f.lblDescription, lblDescription.Events().Click);
                this.BindCommand(ViewModel, x => x.SelectCommand, f => f.imgItem, imgItem.Events().Click);
                this.BindCommand(ViewModel, x => x.SelectCommand, f => f.lblTitle, lblTitle.Events().Click);
                this.BindCommand(ViewModel, x => x.SelectCommand, f => f.tlpMain, tlpMain.Events().Click);
            });
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = SystemColors.ControlLightLight;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = SystemColors.Window;
        }

        internal void OnSelectedChanged() => 
            SelectedChanged?.Invoke(this, EventArgs.Empty);

        public bool Selected
        {
            get => ViewModel.Selected;
        }

        public SelectListItemViewModel ViewModel { get ; set ; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as SelectListItemViewModel; }
    }
}
