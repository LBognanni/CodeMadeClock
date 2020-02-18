using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace CodeMade.ScriptedGraphics
{
    class KnownTypesBinder : ISerializationBinder
    {
        private static Type[] _types = typeof(Canvas).Assembly.GetTypes();
        private static DefaultSerializationBinder _binder = new DefaultSerializationBinder();

        public Type BindToType(string assemblyName, string typeName)
        {
            return _types.SingleOrDefault(t => t.Name == typeName) ?? _binder.BindToType(assemblyName, typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (_types.Any(t => t.FullName == serializedType.FullName))
            {
                assemblyName = null;
                typeName = serializedType.Name;
            }
            else
            {
                _binder.BindToName(serializedType, out assemblyName, out typeName);
            }
        }
    }

}