using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

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
            var files = new Dictionary<string, string>
            {
                {"skinpack.json", @"{
    ""Version"": ""1.2.3"",
    ""Name"": ""Test Pack"",
    ""Description"": ""Test skin pack"",
    ""Skins"": [
        {
          ""Name"": ""A clock"",
          ""Description"": ""A test clock for testing"",
          ""Definition"": ""testclock.json""
        }
    ]
}" },
                {"testclock.json", @"{}" }
            };
            var fileReader = new FakeFileReader(files);
            var sut = SkinPack.Load(fileReader);
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

        [Test]
        public void SkinPack_UpdatesVariables()
        {
            var files = new Dictionary<string, string>
            {
                {"skinpack.json", @"{
    ""Version"": ""1.2.3"",
    ""Name"": ""Test Pack"",
    ""Description"": ""Test skin pack"",
    ""Skins"": [
        {
            ""Name"": ""A clock"",
            ""Description"": ""A test clock for testing"",
            ""Definition"": ""clock.json"",
            ""Variables"": {
                ""col"": ""1"",
                ""color"": ""red""
            }
        }
    ]
}" },
                {"clock.json", @"{
  ""Width"": 100,
  ""Height"": 100,
  ""Layers"": [
    {
      ""$type"": ""SolidLayer"",
      ""BackgroundColor"": ""#fff"",
      ""Shapes"": [
        {
          ""$type"": ""RectangleShape"",
          ""Left"": $col,
          ""Top"": 0.0,
          ""Width"": 50.0,
          ""Height"": 50.0,
          ""Color"": ""$color""
        }
      ],
    }
  ]
}" }
            };
            var fileReader = new FakeFileReader(files);
            var skinpack = SkinPack.Load(fileReader);

            var shape = skinpack.Skins.First().Canvas.Layers.First().Shapes.First() as RectangleShape;
            Assert.IsNotNull(shape);
            Assert.AreEqual("red", shape.Color);
            Assert.AreEqual(1, shape.Left);
            
        }


        class FakeFileReader : IFileReader
        {
            private readonly IDictionary<string, string> _files;

            public FakeFileReader(IDictionary<string, string> files)
            {
                _files = files;
            }

            public bool FileExists(string fileName) =>
                _files.ContainsKey(fileName);

            public string GetFontFile(string fontFile)
            {
                throw new NotImplementedException();
            }

            public string GetString(string fileName) =>
                _files[fileName];
            

            public Image LoadImage(string path)
            {
                throw new NotImplementedException();
            }
        }
    }
}
