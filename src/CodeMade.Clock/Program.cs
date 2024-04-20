using CodeMade.Clock.SkinPacks;
using CodeMade.GithubUpdateChecker;
using CommandLine;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    static class Program
    {
        public static IVersionGetter VersionGetter { get; private set; }

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
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            var settingsPath = Directory.GetParent(Application.LocalUserAppDataPath).FullName;


            try
            {
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                VersionGetter = new GitHubVersionGetter("LBognanni", "CodeMadeClock", "v", v => v.ToString());
                var checker = new VersionChecker(VersionGetter, currentVersion, new WindowsNotification(), new FileBasedTempData("CodeMade.Clock.tmp"), "CodeMade Clock");
                Task.Run(() => checker.NotifyIfNewVersion());
            }
            catch{ }


            var knownTypes = new[]
            {
                typeof(HoursLayer),
                typeof(MinutesLayer),
                typeof(SecondsLayer),
                typeof(NumbersLayer),
                typeof(HourText),
                typeof(MinuteText),
                typeof(SecondText),
                typeof(BlinkLayer),
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
                        Application.Run(new frmClock(settings, skinpacks, options.DisplayFile, knownTypes));
                    }
                })
                .WithNotParsed(errors =>
                {
                    Application.Run(new frmClock(settings, skinpacks, knownTypes: knownTypes));
                });
        }
    }
}
