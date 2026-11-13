using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;

namespace Lab33.Part1;

public static class Serializers
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static void SaveBinary<T>(string path, T obj)
    {
        using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        new BinaryFormatter().Serialize(fs, obj!);
    }

    public static T LoadBinary<T>(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return (T)new BinaryFormatter().Deserialize(fs);
    }

    public static void SaveXml<T>(string path, T obj)
    {
        using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        new XmlSerializer(typeof(T)).Serialize(fs, obj);
    }

    public static T LoadXml<T>(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return (T)new XmlSerializer(typeof(T)).Deserialize(fs)!;
    }

    public static void SaveJson<T>(string path, T obj)
    {
        using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        JsonSerializer.Serialize(fs, obj, JsonOptions);
    }

    public static T LoadJson<T>(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<T>(fs)!;
    }
}
