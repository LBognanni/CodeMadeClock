using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock.Controls
{
    public class SelectList : FlowLayoutPanel
    {
        public SelectList()
        {
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            AutoScroll = true;
        }

        public void Bind<T>(IEnumerable<T> elements, T selected, Func<T, string>titleFn, Func<T, string> descFn, Func<T, Image> imgFn)
        {
            Controls.Clear();
            SelectListItem selectedItem = null;
            foreach (var el in elements)
            {
                var item = new SelectListItem() {
                    Title = titleFn(el),
                    Description = descFn(el),
                    Picture = imgFn(el),
                    Item = el,
                    Width = Width - 100,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                };
                if(el.Equals(selected))
                {
                    item.Selected = true;
                    selectedItem = item;
                }
                item.SelectedChanged += Item_SelectedChanged;
                Controls.Add(item);
            }

            if (selectedItem != null)
                ScrollToControl(selectedItem);
        }

        public IEnumerable<SelectListItem> Items => Controls.OfType<SelectListItem>();

        private void Item_SelectedChanged(object sender, EventArgs e)
        {
            if ((!(sender is SelectListItem item)) || (!item.Selected))
            {
                return;
            }

            foreach (var other in Controls.OfType<SelectListItem>().Where(c => c != item))
            {
                other.Selected = false;
            }
        }

        public T GetSelected<T>()
        {
            foreach (var item in Controls.OfType<SelectListItem>())
            {
                if (item.Selected)
                    return (T)item.Item;
            }

            return default(T);
        }
    }
}
