using CodeMade.Clock.SkinPacks;
using CommandLine;
using System;
using System.IO;
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
            var settingsPath = Directory.GetParent(Application.LocalUserAppDataPath).FullName;

            var knownTypes = new[]
            {
                typeof(HoursLayer),
                typeof(MinutesLayer),
                typeof(SecondsLayer),
                typeof(NumbersLayer),
            };

            var settings = Settings.Load(Path.Combine(settingsPath, "settings.json"));
            var skinpacks = SkinPackCollection.Load(Path.Combine(settingsPath, "skinpacks"), Path.Combine(Application.StartupPath, "clocks"), knownTypes);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    if (!string.IsNullOrEmpty(options.PreviewFile) && File.Exists(options.PreviewFile))
                    {
                            Application.Run(new frmPreview(options.PreviewFile, knownTypes));
                    }
                    else
                    {
                        Application.Run(new frmClock(settings, skinpacks, options.DisplayFile));
                    }
                })
                .WithNotParsed(errors =>
                {
                    Application.Run(new frmClock(settings, skinpacks));
                });
        }
    }
}
