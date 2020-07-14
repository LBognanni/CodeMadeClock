using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeMade.ScriptedGraphics;

namespace CodeMade.Clock.Controls
{
    public partial class SelectListItem : UserControl
    {
        private bool _selected;

        private static Image ImgChecked = GetCircle(true);
        private static Image ImgUnChecked = GetCircle(false);
        
        private static Image GetCircle(bool @checked)
        {
            Canvas canvas = new Canvas(50, 50, "transparent");
            if (@checked)
            {
                canvas.Add(new CircleShape(24, 24, 24, "#00aa12"));
                canvas.Add(new Shape() { Path = "21,32 13,24 11,26 21,36 39,17 37,15", Color = "white" });
            }
            else
            {
                canvas.Add(new CircleShape(24, 24, 24, "#ddd"));
                canvas.Add(new CircleShape(24, 24, 22, "#f2f2f2"));
            }

            return canvas.Render(1);
        }

        public SelectListItem()
        {
            InitializeComponent();
            imgChecked.Click += Control_Click;
            imgItem.Click += Control_Click;
            lblDescription.Click += Control_Click;
            lblTitle.Click += Control_Click;
            tlpMain.Click += Control_Click;
            Selected = false;
        }

        private void Control_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = SystemColors.ControlLightLight;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = SystemColors.Window;
        }

        public string Title { get => lblTitle.Text; set => lblTitle.Text = value; }
        public string Description { get => lblDescription.Text; set => lblDescription.Text = value; }
        public Image Picture { get => imgItem.Image; set => imgItem.Image = value; }

        public bool Selected
        {
            get => _selected; 
            set
            {
                _selected = value;
                imgChecked.Image = value ? ImgChecked : ImgUnChecked;
            }
        }

        public object Item { get; set; }
    }
}
