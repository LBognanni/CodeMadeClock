using System;
using System.Reflection;
using System.Windows.Forms;

namespace CodeMade.Clock.Tests.FormTests
{
    public static class FormExtensions
    {
        public static Control FindControl(this Form form, string controlName) => 
            FindControl(form.Controls, controlName);

        private static Control FindControl(Control.ControlCollection collection, string controlName)
        {
            foreach(Control control in collection)
            {
                if(control.Name.Equals(controlName))
                {
                    return control;
                }
                var child = FindControl(control.Controls, controlName);
                if (child != null)
                    return child;
            }

            return null;
        }

        public static void RunEvent<T>(this T control, string @event, EventArgs eventArgs = null) where T: Control
        {
            var minfo = typeof(T).GetMethod("On" + @event, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            ParameterInfo[] param = minfo.GetParameters();
            Type parameterType = param[0].ParameterType;
            minfo.Invoke(control, new[] { eventArgs ?? Activator.CreateInstance(parameterType) });
        }

        public static void ShowVirtual(this Form form)
        {
            form.Location = new System.Drawing.Point(-10000, -10000);
            form.Show();
        }
    }
}
