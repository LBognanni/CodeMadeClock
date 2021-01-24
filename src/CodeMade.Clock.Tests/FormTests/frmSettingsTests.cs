using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
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
            //_form.ShowVirtual();
        }

        [TearDown]
        public void TearDownTest() => _form.Dispose();

        [Test]
        public void ListsAllSkinPacks_And_SelectsCurrentOne()
        {
            _form.ShowVirtual();
            var cmbSkinPacks = _form.FindControl("cmbSkinPack") as ComboBox;
            Assert.IsNotNull(cmbSkinPacks);
            Assert.AreEqual(2, cmbSkinPacks.Items.Count);
            Assert.AreEqual("test", cmbSkinPacks?.SelectedItem);
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

        [Ignore("manual testing only")]
        [Test]
        public void ShowForm()
        {
            _form.ShowDialog();
        }
    }
}
