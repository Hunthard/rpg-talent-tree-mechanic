using System.IO;
using Huntag.Core;
using UnityEngine;

public class JsonSerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        string json = JsonUtility.ToJson(obj);

        File.WriteAllText(path, json);
    }

    public T Deserialize<T>(string path, ref T obj)
    {
        if (!File.Exists(path)) return default(T);

        string json = File.ReadAllText(path);

        if (obj == null) obj = default(T);

        JsonUtility.FromJsonOverwrite(json, obj);

        return obj;
    }
}
