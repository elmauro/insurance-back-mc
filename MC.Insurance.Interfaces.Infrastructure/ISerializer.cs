using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Interfaces.Infrastructure
{
    public interface ISerializer
    {
        T DeserializeObject<T>(object content) where T : class;
        T DeserializeObject<T>(string _result) where T : class;
        T Parse<T>(string result) where T : class;
        JObject _JObject();
    }
}
