using CodeMade.Clock.SkinPacks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace CodeMade.ScriptedGraphics.Tests
{
    [TestFixture]
    class FileReaderTests
    {
        

        [Test]
        public void JsonNet_Can_Deserialize_Class_With_FileReader_Parameter()
        {
            const string testValue = "the value";
            var resolver = Mock.Of<IFileReader>(
                r => r.GetString(It.IsAny<string>()) == testValue
            );
            var json = @"{ ""Path"": ""test.txt""  }";
            var deserializationSettings = Canvas.GetSerializerSettings(resolver);
            var result = JsonConvert.DeserializeObject<TestPathUser>(json, deserializationSettings);
            Assert.AreEqual(result.Path, "test.txt");
            Assert.AreEqual(result.ResolvedPath, testValue);
        }

        [Test]
        public void Can_Deserialize_Bitmap()
        {
            var json = @"{ ""Path"": ""testimages\\colors.png"", ""Left"": 0, ""Right"": 0, ""Width"": 100, ""Height"": 100 }";
            var resolver = new FileReader(TestContext.CurrentContext.TestDirectory);
            var deserializationSettings = Canvas.GetSerializerSettings(resolver);
            var bmp = JsonConvert.DeserializeObject<BitmapShape>(json, deserializationSettings);
            Assert.IsNotNull(bmp);
            Assert.IsNotNull(bmp.Image.Value);
        }
    }

    public class TestPathUser
    {
        private readonly IFileReader _resolver;
        public string Path { get; set; }
    
        public TestPathUser(IFileReader resolver)
        {
            _resolver = resolver;
        }

        public string ResolvedPath => _resolver.GetString(Path);
    }
}
