using System.Drawing;
using System.Linq;

namespace SimCore
{
    public static class Resources
    {
        public static Image GetImage<T>(string name)
        {
            var type = typeof(T);
            var ass = type.Assembly;
            var names = ass.GetManifestResourceNames();
            var item = names.FirstOrDefault(n => n.EndsWith($".{name}"));
            var stream = ass.GetManifestResourceStream(item)!;
            var image = Image.FromStream(stream);
            return image;
        }
    }
}