using System.IO;
using UnityEngine;

public class SLFromFileManager : MonoBehaviour
{
    public string saveName = "Save";
    public string directory = null;

    private void Awake()
    {
        if (string.IsNullOrEmpty(directory))
        {
            directory = Application.streamingAssetsPath;
        }
    }

    public void Save()
    {
        StopAllCoroutines();
        var result = SaveLoadSystem.GetSave();
        var information = JSONSerializator.GetSerializator().Serialize(result);

        var resultPath = Path.Combine(directory, saveName + ".json");

        using (var writer = new BinaryWriter(File.Open(resultPath,FileMode.Truncate|FileMode.OpenOrCreate)))
        {
            writer.Write(information);
        }
    }

    public void Load()
    {
        StopAllCoroutines();
        
        if(JSONSerializator.GetSerializator().TryDeserialize(File.ReadAllBytes(Path.Combine(directory, saveName + ".json")),out var save))
        {
            SaveLoadSystem.Load((SaveSnapshot)save);
        }
    }

    public void Return()
    {
        StopAllCoroutines();
    }
}