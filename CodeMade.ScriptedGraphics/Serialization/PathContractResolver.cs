using System;
using Newtonsoft.Json.Serialization;

namespace CodeMade.ScriptedGraphics
{
    class PathContractResolver : DefaultContractResolver
    {
        private IPathResolver _resolver;

        public PathContractResolver(IPathResolver resolver)
        {
            _resolver = resolver;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var constructor = objectType.GetConstructor(new Type[] { typeof(IPathResolver) });
            if(constructor!=null)
            {
                var contract = base.CreateObjectContract(objectType);
                contract.DefaultCreator = () => Activator.CreateInstance(objectType, _resolver);
                return contract;
            }

            return base.CreateObjectContract(objectType);
        }
    }

}