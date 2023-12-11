using CodeMade.GithubUpdateChecker;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CodeMade.Clock.Tests.FormTests
{
    public class frmAboutTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenLoading_LoadsNewVersion()
        {
            var fakeVersionGetter = Mock.Of<IVersionGetter>(x =>
                       x.GetLatestVersion() == Task.FromResult(new Version(2, 0, 0, 0)) &&
                                  x.GetReleaseUrl(It.IsAny<Version>()) == "http://test.com");
            var sut = new AboutViewModel(fakeVersionGetter, () => new Version(1, 0, 0, 0));

            Assert.That(sut.NewVersionMessage, Is.EqualTo("A new version is available! Download version 2.0.0.0 here!"));
            Assert.That(sut.NewVersionLink, Is.EqualTo("http://test.com"));
            Assert.That(sut.CurrentVersionMessage, Is.EqualTo("Version 1.0.0.0"));
        }

        [Test]
        public void WhenNoNewVersion_SaysYoureUpToDate()
        {
            var fakeVersionGetter = Mock.Of<IVersionGetter>(x =>
                       x.GetLatestVersion() == Task.FromResult(new Version(1, 0, 0, 0)) &&
                                  x.GetReleaseUrl(It.IsAny<Version>()) == "http://test.com");
            var sut = new AboutViewModel(fakeVersionGetter, () => new Version(1, 0, 0, 0));

            Mock.Get(fakeVersionGetter).Setup(x => x.GetLatestVersion()).Returns(Task.FromResult(new Version(1, 0, 0, 0)));
            Assert.That(sut.NewVersionMessage, Is.EqualTo("You are up to date!"));
            Assert.That(sut.NewVersionLink, Is.Null);
        }
    }
}
