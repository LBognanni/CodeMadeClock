using System;
using Newtonsoft.Json.Serialization;

namespace CodeMade.ScriptedGraphics
{
    class FileReaderContractResolver : DefaultContractResolver
    {
        private IFileReader _fileReader;

        public FileReaderContractResolver(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var constructor = objectType.GetConstructor(new Type[] { typeof(IFileReader) });
            if(constructor!=null)
            {
                var contract = base.CreateObjectContract(objectType);
                contract.DefaultCreator = () => Activator.CreateInstance(objectType, _fileReader);
                return contract;
            }

            return base.CreateObjectContract(objectType);
        }
    }

}