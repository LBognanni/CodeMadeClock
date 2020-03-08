using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeMade.Clock.Tests
{
    public class SerializationTests
    {
        [Test]
        public void CustomConverter_CanDeserializeWithVariables()
        {
            var variables = new Dictionary<string, object>()
            {
                { "$x", 1 },
                { "$radius", 2.0 },
                { "$color", "test" },
                { "$smooth", true }
            };
            var converter = new VariableConverter(variables);

            var json = @"{
    ""Position"": {
        ""X"": ""$x"",
        ""Y"": 0
    },
    ""Color"": ""$color"",
    ""Radius"": ""$radius""
}";
            var obj = JsonConvert.DeserializeObject<CircleShape>(json, converter);
            Assert.AreEqual(1, obj.Position.X);
            Assert.AreEqual("test", obj.Color);
            Assert.AreEqual(2, obj.Radius);
        }
    }
}
