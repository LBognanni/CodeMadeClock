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

namespace CodeMade.Clock
{
    public class SettingsViewModel : ReactiveObject
    {
        private readonly ISettings _settings;
        private readonly SkinPackCollection _skinPacks;
        private readonly IEnumerable<string> _skinPackList;
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

            _skinPackList = _skinPacks.Packs.Keys;

            this.WhenAnyValue(x => x.SelectedSkin).ObserveOn(customMainThreadScheduler).Subscribe(UpdateSelectedSkin);

        }

        public void AddSkinPack(Func<(string Title, string Filter), FileOpenResult> browser)
        {
            throw new NotImplementedException();
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
            foreach(var x in _skinPacks.Packs[packName].Skins)
            {
                var skin = new SelectListItemViewModel
                {
                    Title = x.Name,
                    Description = x.Description,
                    Image = x.Canvas.RenderAt(128, 128),
                    Selected = x.Name == SelectedSkin
                };
                skin.WhenAnyValue(x => x.Selected).Where(x => x).Subscribe(x =>
                {
                    SelectedSkin = skin.Title;
                });
                yield return skin;
            }
        }

        public IEnumerable<string> SkinPackList => _skinPackList;
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
