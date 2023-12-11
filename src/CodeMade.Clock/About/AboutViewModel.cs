using CodeMade.GithubUpdateChecker;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public class AboutViewModel : ReactiveObject
    {
        private readonly IVersionGetter _versionGetter;
        private readonly Func<Version> _getVersion;
        private string _newVersionMessage;
        public string NewVersionMessage
        {
            get => _newVersionMessage;
            set => this.RaiseAndSetIfChanged(ref _newVersionMessage, value);
        }

        private string _newVersionLink;
        public string NewVersionLink
        {
            get => _newVersionLink;
            set => this.RaiseAndSetIfChanged(ref _newVersionLink, value);
        }

        private string _currentVersionMessage;
        public string CurrentVersionMessage
        {
            get => _currentVersionMessage;
            set => this.RaiseAndSetIfChanged(ref _currentVersionMessage, value);
        }

        public AboutViewModel(IVersionGetter versionGetter, Func<Version> getVersion)
        {
            _versionGetter = versionGetter;
            _getVersion = getVersion;
            var checkForUpdatesCommand = ReactiveCommand.CreateFromTask(CheckForUpdates);
            checkForUpdatesCommand.Execute().Subscribe();
        }

        public async Task<Unit> CheckForUpdates()
        {
            var currentVersion = _getVersion();
            CurrentVersionMessage = $"Version {currentVersion}";

            var version = await _versionGetter.GetLatestVersion();
            if (version > currentVersion)
            {
                NewVersionMessage = $"A new version is available! Download version {version} here!";
                NewVersionLink = _versionGetter.GetReleaseUrl(version);
            }
            else
            {
                NewVersionMessage = "You are up to date!";
                NewVersionLink = null;
            }

            return Unit.Default;
        }
    }
}
