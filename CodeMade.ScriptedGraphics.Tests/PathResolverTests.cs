using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.ScriptedGraphics.Tests
{
    [TestFixture]
    class PathResolverTests
    {
        [Test]
        public void JsonNet_Can_Deserialize_Example_Class()
        {
            var resolver = new PathResolver(@"c:\folder\demo");
            var json = @"{ ""Path"": ""test.txt""  }";
            var deserializationSettings = Canvas.GetSerializerSettings(resolver);
            var result = JsonConvert.DeserializeObject<TestPathUser>(json, deserializationSettings);
            Assert.AreEqual(result.Path, "test.txt");
            Assert.AreEqual(result.ResolvedPath, @"c:\folder\demo\test.txt");
        }

        [Test]
        public void Can_Deserialize_Bitmap()
        {
            var json = @"{ ""Path"": ""testimages\\colors.png"", ""Left"": 0, ""Right"": 0, ""Width"": 100, ""Height"": 100 }";
            var resolver = new PathResolver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var deserializationSettings = Canvas.GetSerializerSettings(resolver);
            var bmp = JsonConvert.DeserializeObject<BitmapShape>(json, deserializationSettings);
            Assert.IsNotNull(bmp);
            Assert.IsNotNull(bmp.Image.Value);

        }
    }

    public class TestPathUser
    {
        private readonly IPathResolver _resolver;
        public string Path { get; set; }

        public TestPathUser(IPathResolver resolver)
        {
            _resolver = resolver;
        }

        public string ResolvedPath => _resolver.Resolve(Path);
    }
}
