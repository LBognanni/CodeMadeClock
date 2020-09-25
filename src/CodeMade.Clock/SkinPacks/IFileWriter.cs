using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock.SkinPacks
{
    interface IFileWriter
    {
        void Import(string fileOrDirectoryName);
        void Write(string fileName, string contents);
        void ReplacePack(string oldPack, string newPack);
    }
}
