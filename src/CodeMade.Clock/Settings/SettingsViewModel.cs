using CodeMade.Clock.SkinPacks;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMade.Clock.Controls;
using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive;
using System.Windows.Forms;
using CodeMade.ScriptedGraphics;
using System.IO;
using NodaTime;
using System.Drawing;
using NodaTime.Extensions;

namespace CodeMade.Clock
{
    public class SettingsViewModel : ReactiveObject
    {
        private readonly ISettings _settings;
        private readonly SkinPackCollection _skinPacks;
        private string _selectedSkinPack;
        private string _selectedSkin;
        private ObservableAsPropertyHelper<IEnumerable<SelectListItemViewModel>> _skins;


        public SettingsViewModel(ISettings settings, SkinPackCollection skinPacks, IScheduler customMainThreadScheduler = null)
        {
            customMainThreadScheduler = customMainThreadScheduler ?? RxApp.MainThreadScheduler;
            _settings = settings;
            _skinPacks = skinPacks;
            _selectedSkinPack = _settings.SelectedSkinpack;
            _selectedSkin = _settings.SelectedSkin;

            _skins = this.WhenAnyValue(x => x.SelectedSkinPack)
                .ObserveOn(customMainThreadScheduler)
                .Select(sp => GetSkins(sp).ToList())
                .ToProperty(this, x => x.Skins);

            this.WhenAnyValue(x => x.SelectedSkin).ObserveOn(customMainThreadScheduler).Subscribe(UpdateSelectedSkin);

        }

        public void AddSkinPack(string filePath)
        {
            var directory  = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);
            _skinPacks.Import(new CombinedFileReader(directory), fileName);
        }

        public void SaveChanges()
        {
            _settings.SelectedSkinpack = _selectedSkinPack;
            _settings.SelectedSkin = _selectedSkin;
        }

        public record FileOpenResult(bool Ok, string FileName);

        public IEnumerable<SelectListItemViewModel> SelectedItems { get; }

        private void UpdateSelectedSkin(string selected)
        {
            if (Skins == null)
                return;

            foreach(var item in Skins)
            {
                item.Selected = item.Title == selected;
            }
        }

        private IEnumerable<SelectListItemViewModel> GetSkins(string packName)
        {
            var pack = _skinPacks.Find(packName);

            var fixedTime = new FakeTimer();
            foreach (var x in pack.Skins)
            {
                var skin = new SelectListItemViewModel
                {
                    Title = x.Name,
                    Description = x.Description,
                    Image = RenderFakeTime(fixedTime, x),
                    Selected = x.Name == SelectedSkin
                };
                skin.WhenAnyValue(x => x.Selected).Where(x => x).Subscribe(x =>
                {
                    SelectedSkin = skin.Title;
                });
                yield return skin;
            }
        }

        private Image RenderFakeTime(FakeTimer fixedTime, Skin x)
        {
            var canvas = new ClockCanvas(fixedTime, x.Canvas);
            canvas.Update();
            return canvas.RenderAt(128, 128);
        }

        class FakeTimer : ITimer
        {
            public Instant GetTime() =>
                DateTime.Today.AddHours(10).AddMinutes(10).AddSeconds(33).ToUniversalTime().ToInstant();
        }

        public ObservableCollection<string> SkinPackList => new ObservableCollection<string>(_skinPacks.PackNames);

        public string SelectedSkinPack
        {
            get => _selectedSkinPack;
            set => this.RaiseAndSetIfChanged(ref _selectedSkinPack, value);
        }
        public string SelectedSkin
        {
            get => _selectedSkin;
            set => this.RaiseAndSetIfChanged(ref _selectedSkin, value);
        }
        public IEnumerable<SelectListItemViewModel> Skins => _skins?.Value;
    }
}
