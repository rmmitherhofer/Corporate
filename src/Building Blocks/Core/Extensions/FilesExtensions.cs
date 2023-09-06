using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Extensions;

public static class FilesExtensions
{
    public static T JsonToObject<T>(string path)
    {
        var obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(path, Encoding.GetEncoding("UTF-8")));

        return obj;
    }
}
