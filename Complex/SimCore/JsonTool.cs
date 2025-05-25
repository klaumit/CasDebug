using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SimHash
{
    public class JsonTool
    {
        public static void WriteJson(object val, string file)
        {
            var json = JsonConvert.SerializeObject(val, GetConfig());
            File.WriteAllText(file, json, Encoding.UTF8);
        }

        public static T ReadJson<T>(string file)
        {
            if (!File.Exists(file)) return Activator.CreateInstance<T>();
            var json = File.ReadAllText(file, Encoding.UTF8);
            return JsonConvert.DeserializeObject<T>(json, GetConfig());
        }

        private static JsonSerializerSettings GetConfig()
        {
            var config = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore
            };
            return config;
        }
    }
}