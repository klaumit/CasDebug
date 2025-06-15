using System;
using System.IO;
using System.Linq;

namespace SimCore
{
    public static class SimTool
    {
        public static string ToLabel(string file)
        {
            var dir = Path.GetDirectoryName(file)!;
            if (dir.EndsWith("\\SIM", StringComparison.InvariantCultureIgnoreCase))
                dir = Path.GetDirectoryName(dir);
            var name = Path.GetFileName(dir);
            return name;
        }

        public static OneLoad GetLoadedProject(ISimExeItem sim)
        {
            var text = sim.Main.Text;

            var tmp = "SIM3022";
            if (text.StartsWith(tmp))
            {
                var sim86File = text.Substring(tmp.Length).TrimStart(' ', '-');
                if (sim86File.Length >= 1)
                {
                    var model = Path.GetFileNameWithoutExtension(sim86File);
                    var sdk = ToLabel(sim86File);
                    return new OneLoad(sim86File, model, sdk);
                }
            }

            tmp = " - CASIO SimSH Simulator";
            if (text.EndsWith(tmp))
            {
                var o = StringSplitOptions.None;
                var simShTxtA = text.Split([tmp], 2, o);
                if (simShTxtA.Length == 2)
                {
                    var simShTxt = simShTxtA[0];
                    tmp = "New project ";
                    if (!simShTxt.StartsWith(tmp))
                    {
                        var parts = simShTxt.Split([" ("], 2, o);
                        var partM = parts[0];
                        var model = parts[1].TrimEnd(')');
                        var file = sim.Projects.FirstOrDefault(s => Path.GetFileNameWithoutExtension(s).Equals(partM));
                        if (file != null)
                        {
                            var sdk = ToLabel(file);
                            return new OneLoad(file, model, sdk);
                        }
                    }
                }
            }

            return null;
        }

        public static SimExeKind GetExeKind(string file)
        {
            var simple = Path.GetFileNameWithoutExtension(file);
            simple = simple.Replace("CASIO ", "");
            var kind = (SimExeKind)Enum.Parse(typeof(SimExeKind), simple);
            return kind;
        }
    }
}