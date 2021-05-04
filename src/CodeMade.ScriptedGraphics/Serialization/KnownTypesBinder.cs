using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace CodeMade.ScriptedGraphics
{
    class KnownTypesBinder : ISerializationBinder
    {
        private static Type[] _defaultTypes = typeof(Canvas).Assembly.GetTypes();
        private static DefaultSerializationBinder _binder = new DefaultSerializationBinder();
        private Type[] _knownTypes;

        public KnownTypesBinder(Type[] knownTypes)
        {
            _knownTypes = knownTypes.Union(_defaultTypes).ToArray();
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return _knownTypes.SingleOrDefault(t => t.Name == typeName) ?? _binder.BindToType(assemblyName, typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (_knownTypes.Any(t => t.FullName == serializedType.FullName))
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