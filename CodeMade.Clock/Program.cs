using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if ((args.Length > 0) && (System.IO.File.Exists(args[0])))
            {

                Application.Run(new frmClock(args[0]));
                //Application.Run(new frmPreview(args[0]));
            }

            else
            {
                Application.Run(new frmClock(null));
            }
        }
    }
}
