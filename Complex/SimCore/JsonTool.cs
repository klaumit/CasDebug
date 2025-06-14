using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SimCore
{
    public static class JsonTool
    {
        public static string WritePlain(object val)
        {
            var res = JsonConvert.SerializeObject(val, GetConfig());
            return res!;
        }

        public static void WriteJson(object val, string file)
        {
            var json = WritePlain(val);
            File.WriteAllText(file, json, Encoding.UTF8);
        }

        public static T ReadPlain<T>(string json)
        {
            var res = JsonConvert.DeserializeObject<T>(json, GetConfig());
            return res!;
        }

        public static T ReadJson<T>(string file)
        {
            if (!File.Exists(file)) return Activator.CreateInstance<T>();
            var json = File.ReadAllText(file, Encoding.UTF8);
            var res = ReadPlain<T>(json);
            return res!;
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