using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MineManagementWPF
{
    public static class JsonConvert
    {
        public static string SerializeObject<TValue>(TValue value)
        {
            MemoryStream stream = new MemoryStream();
            JsonSerializer.Serialize<TValue>(stream, value);
            string json = Encoding.UTF8.GetString(stream.ToArray());

            return json;
        }

        public static string SerializeObject(object value)
        {
            MemoryStream stream = new MemoryStream();
            JsonSerializer.Serialize(stream, value);
            string json = Encoding.UTF8.GetString(stream.ToArray());

            return json;
        }

        public static TOutput DeserializeObject<TOutput>(string json)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var output = JsonSerializer.Deserialize<TOutput>(stream);

            return output;
        }

        public static object DeserializeObject(string json)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var output = JsonSerializer.Deserialize<object>(stream);

            return output;
        }
    }
}
