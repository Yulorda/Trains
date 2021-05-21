using System.Collections.Generic;
using UnityEngine;

public class PrefabComponentFactory<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    protected T prefab;

    public IEnumerable<T> Create(int count)
    {
        List<T> gameObjects = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(prefab, transform);
            Config(go);
            gameObjects.Add(go);
        }

        return gameObjects;
    }

    public T Create()
    {
        var go = Instantiate(prefab, transform);
        Config(go);
        return go;
    }

    protected virtual void Config(T obj)
    {
    }
}