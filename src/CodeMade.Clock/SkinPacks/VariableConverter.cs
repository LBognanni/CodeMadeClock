using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMade.Clock.SkinPacks
{
    public class VariableConverter : JsonConverter
    {
        private readonly IDictionary<string, object> _variables;
        static readonly Type[] AcceptedTypes =
        {
                typeof(int),
                typeof(string),
                typeof(decimal),
                typeof(float),
                typeof(double),
                typeof(bool),
            };

        public VariableConverter(IDictionary<string, object> variables)
        {
            _variables = variables;
        }

        public override bool CanConvert(Type objectType)
        {
            return AcceptedTypes.Contains(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string s)
            {
                if (s.StartsWith("$"))
                {
                    if (_variables.TryGetValue(s, out var value))
                    {
                        return Convert.ChangeType(value, objectType);
                    }
                }
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}