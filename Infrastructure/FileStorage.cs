using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace EventManagement.Infrastructure
{
    public static class FileStorage
    {
        public static List<T> Load<T>(string file)
        {
            if (!File.Exists(file)) return new List<T>();
            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(file));
        }
        public static void Save<T>(string file, List<T> data)
        {
            File.WriteAllText(file, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
