using CodeMade.Clock.SkinPacks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.Tests.FormTests
{
    [TestFixture]
    class frmClockTests
    {
        private frmClock _sut;
        private ISettings _settings;
        private SkinPackCollection _skinpacks;

        [SetUp]
        public void Setup()
        {
            _settings = Mock.Of<ISettings>(s =>
                s.HasSettings == true &&
                s.SelectedSkinpack == "test" &&
                s.SelectedSkin == "Red");
            _skinpacks = new SkinPackCollection(TestHelpers.GetFakeFileReader());

        }

        [Test]
        public void frmClock_LoadsSelectedSkinpack()
        {
            Assert.Fail("Add a way to check the loaded skin/pack");
        }

        [Test]
        public async Task frmClock_SavesSettingsOnceWithMultipleResizes()
        {
            var sut = new frmClock(settings: _settings, skinpacks: _skinpacks);
            sut.Width++;
            sut.Height++;
            sut.Width += 10;

            await Task.Delay(550).ConfigureAwait(false);

            Mock.Get(_settings).Verify(s => s.Save(), Times.Once);
        }
    }
}
