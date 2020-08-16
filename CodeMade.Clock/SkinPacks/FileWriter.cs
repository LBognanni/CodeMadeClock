using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.SkinPacks
{
    class FileWriter : IFileWriter
    {
        private readonly string _path;

        public FileWriter(string path) => 
            _path = path;

        public void Import(string fileOrDirectoryName)
        {
            if (Directory.Exists(fileOrDirectoryName))
            {
                CopyFolder(fileOrDirectoryName, Path.Combine(_path, Path.GetFileName(fileOrDirectoryName)));
            }
            else if (File.Exists(fileOrDirectoryName))
            {
                File.Copy(fileOrDirectoryName, _path);
            }
            else
            {
                throw new FileNotFoundException("File or directory does not exist.");
            }
        }

        public static void CopyFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            foreach (var directory in Directory.GetDirectories(sourceFolder))
            {
                CopyFolder(directory, Path.Combine(destinationFolder, Path.GetFileName(directory)));
            }

            foreach (var file in Directory.GetFiles(sourceFolder))
            {
                string destinationFile = Path.Combine(destinationFolder, Path.GetFileName(file));
                File.Copy(file, destinationFile);
            }
        }

        public void Write(string fileName, string contents)
        {
            File.WriteAllText(Path.Combine(_path, fileName), contents);
        }
    }
}
