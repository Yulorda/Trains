using System.IO;
using UnityEngine;
using Zenject;

public class SLFromFileManager : MonoBehaviour
{
    public string saveName = "Save";
    public string directory = null;

    [Inject]
    SaveLoadSystem saveLoadSystem;
    [Inject]
    JSONSerializator jSONSerializator;

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
        var result = saveLoadSystem.GetSave();
        var information = jSONSerializator.Serialize(result);

        var resultPath = Path.Combine(directory, saveName + ".json");

        using (var writer = new BinaryWriter(File.Open(resultPath,FileMode.Truncate|FileMode.OpenOrCreate)))
        {
            writer.Write(information);
        }
    }

    public void Load()
    {
        StopAllCoroutines();
        
        if(jSONSerializator.TryDeserialize(File.ReadAllBytes(Path.Combine(directory, saveName + ".json")),out var save))
        {
            saveLoadSystem.Load((SaveSnapshot)save);
        }
    }

    public void Return()
    {
        StopAllCoroutines();
    }
}