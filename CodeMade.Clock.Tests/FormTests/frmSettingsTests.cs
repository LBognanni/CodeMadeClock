using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            var filereader = GetFakeFileReader();
            var skinPack = new SkinPackCollection(filereader);
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
            var lvwSkins = _form.FindControl("lvwSkins") as ListView;
            Assert.IsNotNull(lvwSkins);
            Assert.AreEqual(4, lvwSkins.Items.Count);
            Assert.AreEqual(1, lvwSkins.SelectedItems.Count);
            Assert.AreEqual("Blue", lvwSkins.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.Text);
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
            var lvwSkins = _form.FindControl("lvwSkins") as ListView;
            Assert.IsNotNull(cmdSave);

            _form.ShowVirtual();
            lvwSkins.Items[3].Selected = true;

            cmdSave.RunEvent("Click");
            Assert.IsFalse(_form.Visible);
            Assert.AreEqual("Black", _settings.SelectedSkin);
        }

        [Test]
        public void ShowForm()
        {
            _form.ShowDialog();
        }

        private IFileReader GetFakeFileReader()
        {
            string skinpack = @"{
	""Version"": ""1.0"",
	""Name"": ""test"",
	""Description"": ""Test pack"",
	""Skins"": [
		{
			""Name"": ""Red"",
			""Description"": """",
			""Definition"": ""clockred.json""
		},
		{
			""Name"": ""Blue"",
			""Description"": ""Et nihil aut tempora laudantium hic esse dolor et. Nihil praesentium repellat non natus placeat blanditiis omnis accusantium. Non qui aperiam quis asperiores illo. Nihil qui ut ut quidem. Non voluptas qui impedit minima quisquam perferendis aut "",
			""Definition"": ""clockblue.json""
		},
		{
			""Name"": ""Green"",
			""Description"": """",
			""Definition"": ""clockgreen.json""
		},
		{
			""Name"": ""Black"",
			""Description"": """",
			""Definition"": ""clockblack.json""
		},
	]
}";
            return new FakeFileReader(new Dictionary<string, string>
            {
                { "skinpacks.json", "[\"test\", \"demo\"]" },
                { "skinpack.json", skinpack },
                { "clockred.json", Canvas("red") },
                { "clockblue.json", Canvas("blue") },
                { "clockgreen.json", Canvas("green") },
                { "clockblack.json", Canvas("#333") }
            });
        }

        private string Canvas(string color)
        {
            return @"{
    ""Width"": 101,
    ""Height"": 101,
    ""Layers"": [
        {
            ""Shapes"": [
                {
                    ""$type"": ""CircleShape"",
                    ""Position"": {
                        ""X"": 49,
                        ""Y"": 49
                    },
                    ""Radius"": 48,
                    ""Height"": 48,
                    ""Color"": """ + color + @"""
                }
            ]
        }
    ]
}";
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
