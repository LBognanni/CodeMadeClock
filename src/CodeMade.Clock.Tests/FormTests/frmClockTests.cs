using CodeMade.Clock.LocationMoving;
using CodeMade.Clock.SkinPacks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.Tests.FormTests
{
    [TestFixture]
    class frmClockTests
    {
        private ISettings _settings;
        private SkinPackCollection _skinpacks;
        private ITimer _timer;
        private ILocationFixer _locationFixer;
        private frmClockViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _settings = Mock.Of<ISettings>(s =>
                s.HasSettings == true &&
                s.SelectedSkinpack == "test" &&
                s.SelectedSkin == "Red" &&
                s.Size == new Size(100, 100) &&
                s.Location == new Point(200, 200));
            _skinpacks = new SkinPackCollection(TestHelpers.GetFakeFileReader(), null, Array.Empty<Type>());
            _timer = Mock.Of<ITimer>();
            
            _locationFixer = Mock.Of<ILocationFixer>();
            Mock.Get(_locationFixer).Setup(x => x.FixLocation(It.IsAny<Point>(), It.IsAny<Size>())).Returns<Point, Size>((x,s) => x);

            _sut = new frmClockViewModel(_settings, _skinpacks, _timer, _locationFixer, throttleTimeInMs: 50);
        }

        [Test]
        public void ViewModel_LoadsSelectedSkinpack()
        {
           // Assert.AreEqual("Red", sut);
        }

        [Test]
        public async Task frmClock_SavesSettingsOnceWithMultipleResizes()
        {
            _sut.Width++;
            _sut.Height++;
            _sut.Width += 10;

            await Task.Delay(100).ConfigureAwait(false);

            Mock.Get(_settings).Verify(s => s.Save(), Times.Once);
        }

        [Test]
        public async Task WhenBigger_WidthAndHeightAreChanged()
        {
            _sut.Bigger();

            await Task.Delay(100).ConfigureAwait(false);

            Assert.AreEqual(125, _settings.Size.Width);
            Assert.AreEqual(125, _settings.Size.Height);
            Mock.Get(_settings).Verify(s => s.Save(), Times.Once);
        }

        [Test]
        public async Task WhenSmaller_WidthAndHeightAreChanged()
        {
            _sut.Smaller();

            await Task.Delay(100).ConfigureAwait(false);

            Assert.AreEqual(80, _settings.Size.Width);
            Assert.AreEqual(80, _settings.Size.Height);
            Mock.Get(_settings).Verify(s => s.Save(), Times.Once);
        }
    }
}
