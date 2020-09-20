using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeMade.Clock.Tests.FormTests
{
    class TestHelpers
    {
        public static IFileReader GetFakeFileReader()
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
                { "test/skinpack.json", skinpack },
                { "demo/skinpack.json", @"{
	""Version"": ""1.0"",
	""Name"": ""demo""}" },
                { "test/clockred.json", Canvas("red") },
                { "test/clockblue.json", Canvas("blue") },
                { "test/clockgreen.json", Canvas("green") },
                { "test/clockblack.json", Canvas("#333") }
            });
        }

        private static string Canvas(string color)
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
    }
}
