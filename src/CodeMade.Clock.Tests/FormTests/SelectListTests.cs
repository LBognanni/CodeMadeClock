using CodeMade.Clock.Controls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI.Testing;
using CodeMade.Clock.SkinPacks;
using Microsoft.Reactive.Testing;
using System.Reactive.Concurrency;

namespace CodeMade.Clock.Tests.FormTests
{

    class SelectListItemViewModelTests
    {
        [Test]
        public void WhenItemIsSelected_CheckImageIsChecked()
        {
            var sut = new SelectListItemViewModel { Title = "Test", Selected = false };
            Assert.AreEqual(SelectListItemViewModel.UncheckedImage, sut.CheckImage);


            sut.Selected = true;

            Assert.AreEqual(SelectListItemViewModel.CheckedImage, sut.CheckImage);
        }

    }

    class SettingsViewModelTests
    {
        private Func<TestScheduler, SettingsViewModel> _getSut;

        [SetUp]
        public void Setup()
        {
            var filereader = TestHelpers.GetFakeFileReader();
            var skinPack = new SkinPackCollection(filereader, null);

            _getSut = (s) => {
                var sut = new SettingsViewModel(new FakeSettings() { SelectedSkinpack = "test", SelectedSkin = "Black" }, skinPack, s);
                s.AdvanceByMs(2 * 100);
                return sut;
            };
        }

        [Test]
        public void InitialSanityCheck()
        {
            new TestScheduler().With(s =>
            {
                var _sut = _getSut(s);

                Assert.NotNull(_sut.Skins);
                Assert.AreEqual(4, _sut.Skins.Count());
                Assert.AreEqual("Black", _sut.SelectedSkin);
                Assert.IsTrue(_sut.Skins.First(s => s.Title == "Black").Selected);
            });
        }

        [Test]
        public void WhenChangingSelectedSkin_SkinIsSelected()
        {
            new TestScheduler().With(s =>
            {
                var _sut = _getSut(s);
                _sut.SelectedSkin = "Red";
                s.AdvanceByMs(100);
                Assert.IsFalse(_sut.Skins.First(s => s.Title == "Black").Selected);
                Assert.IsTrue(_sut.Skins.First(s => s.Title == "Red").Selected);
            });
        }

        [Test]
        public void WhenChangingSelectedSkinFromList_PropertyChanges()
        {
            new TestScheduler().With(s =>
            {
                var _sut = _getSut(s);
                _sut.Skins.First(s => s.Title == "Red").Selected = true;
                s.AdvanceByMs(100);
                Assert.AreEqual("Red", _sut.SelectedSkin);
                Assert.IsFalse(_sut.Skins.First(s => s.Title == "Black").Selected);
            });
        }
    }

}
