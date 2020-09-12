using MC.Insurance.Interfaces.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MC.Insurance.Infrastructure
{
    public class Serializer : ISerializer
    {
        public Serializer() { }

        public T DeserializeObject<T>(object content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content.ToString());
        }

        public T DeserializeObject<T>(string _result) where T : class
        {
            return JsonConvert.DeserializeObject<T>(_result);
        }

        public T Parse<T>(string result) where T : class
        {
            return JObject.Parse(result).ToObject<T>();
        }

        public JObject _JObject()
        {
            return new JObject();
        }
    }
}
