using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeMade.Clock.Controls;

namespace CodeMade.Clock.Tests.FormTests
{
    class frmSettingsTests
    {
        private frmSettings _form;
        private ISettings _settings;

        [OneTimeSetUp]
        public void Setup() =>
            Application.EnableVisualStyles();

        [SetUp]
        public void SetupTest()
        {
            _settings = new FakeSettings() { SelectedSkinpack = "test", SelectedSkin = "Black" };
            var filereader = TestHelpers.GetFakeFileReader();
            var skinPack = new SkinPackCollection(filereader, null);
            _form = new frmSettings(skinPack, _settings);
        }

        [TearDown]
        public void TearDownTest() => _form.Dispose();

        [Test]
        public void ListsAllSkinPacks_And_SelectsCurrentOne()
        {
            var cmbSkinPacks = _form.FindControl("cmbSkinPack") as ComboBox;
            Assert.IsNotNull(cmbSkinPacks);
            Assert.AreEqual(2, cmbSkinPacks.Items.Count);
            Assert.AreEqual("test", ((SkinPack)cmbSkinPacks?.SelectedItem)?.Name);
        }

        [Test]
        public void ListsAllSkins_And_SelectsCurrentOne()
        {
            var slSkins = _form.FindControl("slSkins") as SelectList;
            Assert.IsNotNull(slSkins);
            Assert.AreEqual(4, slSkins.Items.Count());
            Assert.AreEqual("Black", slSkins.GetSelected<Skin>().Name);
        }

        [Test]
        public void Cancel_ClosesTheForm()
        {
            var cmdCancel = _form.FindControl("cmdCancel");
            Assert.IsNotNull(cmdCancel);

            _form.ShowVirtual();
            cmdCancel.RunEvent("Click");
            Assert.IsFalse(_form.Visible);
        }

        [Test]
        public void Save_AppliesChangesAndClosesTheForm()
        {
            var cmdSave = _form.FindControl("cmdSave");
            var slSkins = _form.FindControl("slSkins") as SelectList;
            Assert.IsNotNull(cmdSave);

            _form.ShowVirtual();
            slSkins.Items.First(i=>i.Title == "Black").Selected = true;

            cmdSave.RunEvent("Click");
            Assert.IsFalse(_form.Visible);
            Assert.AreEqual("Black", _settings.SelectedSkin);
        }

        [Ignore("manual testing only")]
        [Test]
        public void ShowForm()
        {
            _form.ShowDialog();
        }

        class FakeSettings : ISettings
        {
            public bool HasSettings => true;

            public Point Location { get; set; }
            public string SelectedSkin { get; set; }
            public string SelectedSkinpack { get; set; }
            public Size Size { get; set; } = new Size(100, 100);

            public void Save()
            {
            }
        }
    }
}
