﻿using CommandLine;
using System;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    static class Program
    {
        class Options
        {
            [Value(0, Required = false)]
            public string DisplayFile { get; set; }
            [Option('p', "preview", Required = false, HelpText = "Run in preview mode showing the specified file")]
            public string PreviewFile { get; set; }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    if (!string.IsNullOrEmpty(options.PreviewFile) && System.IO.File.Exists(options.PreviewFile))
                    {
                        Application.Run(new frmPreview(options.PreviewFile));
                    }
                    else
                    {
                        Application.Run(new frmClock(options.DisplayFile));
                    }
                })
                .WithNotParsed(errors =>
                {
                    Application.Run(new frmClock(null));
                });
        }
    }
}
