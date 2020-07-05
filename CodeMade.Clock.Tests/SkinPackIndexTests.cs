using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeMade.Clock.Tests
{
    [TestFixture]
    public class SkinPackIndexTests
    {
        [Test]
        public void SkinPackIndex_CanBeDeserializedInto()
        {
            var json = @"{
    ""Version"": ""1.2.3"",
    ""Name"": ""Test Pack"",
    ""Description"": ""Test skin pack"",
    ""Skins"": [
    ]
}";
            var sut = new SkinPack(null);
            JsonConvert.PopulateObject(json, sut);
            Assert.AreEqual("1.2.3", sut.Version.ToString());
            Assert.AreEqual("Test Pack", sut.Name);
            Assert.IsNotEmpty(sut.Description);
            Assert.IsEmpty(sut.Skins);
        }

        [Test]
        public void SkinPackIndex_CanBeLoaded()
        {
            var sut = SkinPack.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "testpack"));
            Assert.IsNotNull(sut);
            Assert.AreEqual(1, sut.Skins.Count);
            var skin = sut.Skins[0];
            Assert.AreEqual("A clock", skin.Name);
            Assert.AreEqual("testclock.json", skin.Definition);
            Assert.IsEmpty(skin.Variables);
        }

        [Test]
        public void SkinPack_ThrowsWhenIndexIsMissing()
        {
            var io = Mock.Of<IFileReader>(r => r.FileExists(It.IsAny<string>()) == false);
            Assert.Throws<EntryPointNotFoundException>(() => {
                SkinPack.Load(io);
            });
        }

        [Test]
        public void SkinPack_ThrowsWhenSkinFileIsMissing()
        {
            string skinpack = @"{
    ""Version"": ""1.2.3"",
    ""Name"": ""Test Pack"",
    ""Description"": ""Test skin pack"",
    ""Skins"": [
        {
          ""Name"": ""A clock"",
          ""Description"": ""A test clock for testing"",
          ""Definition"": ""skin.json""
        }
    ]
}";

            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns((string s) => s == "skinpack.json");
            fileReaderMock.Setup(f => f.GetString("skinpack.json")).Returns(skinpack);

            Assert.Throws<SkinPack.ValidationFailedException>(() =>
            {
                SkinPack.Load(fileReaderMock.Object);
            });
        }
    }
}
