using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    internal class ZipFileReader : IFileReader, IDisposable
    {
        private ZipArchive _zipFile;
        private List<string> _filesToDelete;

        public ZipFileReader(string zipFileName)
        {
            _filesToDelete = new List<string>();
            _zipFile = ZipFile.OpenRead(zipFileName);
        }

        public void Dispose()
        {
            _filesToDelete?.ForEach(f => File.Delete(f));
            _zipFile?.Dispose();
        }

        public bool FileExists(string fileName) => 
            _zipFile.Entries.Any(e => e.FullName.Equals(fileName));

        public string GetFontFile(string fontFile)
        {
            var tempFile = Path.GetTempFileName();
            _filesToDelete.Add(tempFile);
            var entry = _zipFile.Entries.FirstOrDefault(x => x.Name.Equals(fontFile, StringComparison.OrdinalIgnoreCase));
            if (entry != null)
            {
                entry.ExtractToFile(tempFile, true);
            }
            else
            {
                Debugger.Break();
            }
            return tempFile;
        }

        public IFileReader GetPack(string pack) =>
            throw new NotSupportedException("Reading a package from inside a ZipFile is not supported.");

        public string GetString(string fileName)
        {
            var entry = _zipFile.GetEntry(fileName);
            using var s = entry.Open();
            using var reader = new StreamReader(s);
            return reader.ReadToEnd();
        }

        public Image LoadImage(string path)
        {
            var entry = _zipFile.GetEntry(path);
            using var s = entry.Open();
            return Bitmap.FromStream(s);
        }

        public bool PackExists(string pack) => false;

        public string Resolve(string fileName) =>
            throw new NotSupportedException("Resolving is not supported in Zip files");
    }
}
