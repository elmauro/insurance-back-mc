using Newtonsoft.Json.Linq;

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
