using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    static class Extensions
    {
        public static Action Debounce(this Action func, int milliseconds = 300)
        {
            CancellationTokenSource? cancelTokenSource = null;

            return () =>
            {
                cancelTokenSource?.Cancel();
                cancelTokenSource = new CancellationTokenSource();

                Task.Delay(milliseconds, cancelTokenSource.Token)
                    .ContinueWith(t =>
                    {
                        if (!t.IsCanceled)
                        {
                            func();
                        }
                    }, TaskScheduler.Default);
            };
        }

        public static bool SelectItem(this ComboBox comboBox, object selectedItem)
        {
            if (selectedItem == null)
            {
                return false;
            }

            var item = comboBox.Items.IndexOf(selectedItem);
            if(item >=0)
            {
                comboBox.SelectedIndex = item;
                return true;
            }

            return false;
        }

    }
}
