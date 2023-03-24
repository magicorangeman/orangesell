using System.Text.Json.Nodes;

namespace JsonClient
{
    internal class FileManager
    {
        public static FileInfo[] GetFilesForSend(string path, DateTime startTime = default)
        {
            var files = Directory.GetFiles(path);
            return files
                .Select(fileName => new FileInfo(fileName))
                .Where(file => file.Extension == ".json")
                .Where(file => file.CreationTime > startTime)
                .ToArray();
        }

        public static JsonList[] GetJsonTypes(params FileInfo[] files)
        {
            return files
               .SelectMany(file => File.ReadAllLines(file.FullName))
               .Where(line => line != "")
               .Select(line => JsonNode.Parse(line))
               .GroupBy(json => json["type"].ToString())
               .Select(group => (group.Key, group.Count()))
               .Select(tuple => new JsonList { Type = tuple.Item1.ToString(), Number = tuple.Item2 })
               .ToArray();
        }

        public static JsonNode?[] GetJsonArray(string type, params FileInfo[] files)
        {
            return files
               .SelectMany(file => File.ReadAllLines(file.FullName))
               .Where(line => line != "")
               .Select(line => JsonNode.Parse(line))
               .Where(json => json["type"].ToString() == type)
               .ToArray();
        }
    }
}
