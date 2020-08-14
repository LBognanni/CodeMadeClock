using NUnit.Framework;
using System.IO;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class ZipFileReaderTests
    {
        private ZipFileReader _sut;
        
        [SetUp]
        public void Setup()
        {
            _sut = new ZipFileReader(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.zip"));
        }

        [Test]
        public void ZipFileReader_CanReadString()
        {
            var json = _sut.GetString("test.json");
            Assert.IsNotNull(json);
            Assert.IsTrue(json.StartsWith("{") && json.EndsWith("}"));
        }

        [Test]
        public void ZipFileReader_CanReadBitmap()
        {
            var bmp = _sut.LoadImage("text.png");
            Assert.IsNotNull(bmp);
            bmp.Dispose();
        }

        [Test]
        public void ZipFileReader_CanSayIfFileExists()
        {
            Assert.IsTrue(_sut.FileExists("test.json"));
            Assert.IsFalse(_sut.FileExists("folder/file.txt"));
        }
    }
}
