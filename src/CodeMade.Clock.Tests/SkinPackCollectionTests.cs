using CodeMade.Clock.SkinPacks;
using CodeMade.ScriptedGraphics;
using Microsoft.VisualBasic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeMade.Clock.Tests
{
    class SkinPackCollectionTests
    {
        private SkinPackCollection _sut;
        const string skinpackname = "a_skinpack";

        [SetUp]
        public void Setup()
        {
            var files = new Dictionary<string, string>() {
                { "skinpacks.json", $"[ \"{skinpackname}\" ]" },
                { $"{skinpackname}/skinpack.json", "{ \"Name\": \"" + skinpackname + "\" }" }
            };
            _sut = new SkinPackCollection(new FakeFileReader(files), new FakeFileWriter(), Array.Empty<Type>());
        }

        [Test]
        public void SkinPackCollection_ImportRequiresExistingFile()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                _sut.Import(new FakeFileReader(new Dictionary<string, string>()), "nonexisting.file");
            });
        }

        [Test]
        public void SkinPackCollection_CantImportSamePack()
        {
            var files = new Dictionary<string, string>() {
                { "testpack", "" },
                { "testpack/skinpack.json", "{ \"Name\": \"" + skinpackname + "\" }" }
            };

            Assert.Throws<SkinPackCollection.DuplicatePackException>(() =>
            {
                _sut.Import(new FakeFileReader(files), "testpack");
            });
        }

        [Test]
        public void SkinPackCollection_CanImportNewPack()
        {
            var files = new Dictionary<string, string>() {
                { "other pack", "" },
                { "other pack/skinpack.json", "{ \"Name\": \"other pack\" }" }
            };

            _sut.Import(new FakeFileReader(files), "other pack");
            CollectionAssert.Contains(_sut.Packs.Keys, "other pack");
        }
    }
}
