using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimLoad.Imports;
using D = System.Collections.Generic.SortedDictionary<string, SimLoad.Core.DisAsmItem>;

#if NETFRAMEWORK
using ConvertX = SimCore.HexTool;
#else
using ConvertX = System.Convert;
#endif

// ReSharper disable LocalizableElement

namespace SimLoad.Core
{
    public static class Applet
    {
        public static void DisassembleToJson()
        {
            const string file = "UnAsmSys.json";
            var dict = ReadJson<D>(file);
            var start = dict.Count;
            for (int i = short.MinValue; i <= short.MaxValue; i++)
                Disassemble(dict, i);
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
                Disassemble(dict, GetNextInt64(rnd));
            WriteJson(dict, file);
            var end = dict.Count;
            StartFile(file);
            MessageBox.Show($"Found {start} to {end}!", nameof(UnAsmSys));
        }

        private static long GetNextInt64(Random rnd)
        {
#if NETFRAMEWORK
            return rnd.Next(int.MinValue, int.MaxValue);
#else
            return rnd.NextInt64();
#endif
        }

        private static void Disassemble(D dict, long value)
        {
            var bytes = BitConverter.GetBytes(value);
            var hex = ConvertX.ToHexString(bytes);
            if (dict.ContainsKey(hex))
                return;
            var res = UnAsmSys.DisAsmLine(bytes);
            var text = res?.Text ?? "";
            var size = res?.Len ?? 0;
            var o = StringSplitOptions.None;
            var parts = text.Split(["  "], 2, o);
            DisAsmItem item = parts.Length == 2
                ? new(parts[0].Trim(), parts[1].Trim(), size)
                : new("", "", size);
            dict[hex] = item;
        }

        private static void StartFile(string file)
        {
            var info = new ProcessStartInfo
            {
                FileName = file, UseShellExecute = true
            };
            var proc = Process.Start(info);
            proc!.WaitForInputIdle();
        }

        private static void WriteJson(object val, string file)
        {
            var json = JsonConvert.SerializeObject(val, GetConfig());
            File.WriteAllText(file, json, Encoding.UTF8);
        }

        private static T ReadJson<T>(string file)
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

        public static string GetPathOf(Type type)
        {
            var ass = Path.GetFullPath(type.Assembly.Location);
            var dir = Path.GetDirectoryName(ass)!;
            var o = StringSplitOptions.None;
            var root = dir.Split(["\\bin\\"], 2, o).First();
            return root;
        }
    }
}