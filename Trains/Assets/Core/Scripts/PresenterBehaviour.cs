using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PresenterBehaviour<T> : MonoBehaviour, IDisposable
{
    public T model;
    private List<IDisposable> disposables = new List<IDisposable>();

    public void InjectModel(T model)
    {
        if (this.model != null)
            RemoveModel();

        this.model = model;
        OnInjectModel();
    }

    public void RemoveModel()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
        disposables.Clear();

        OnRemoveModel();
    }

    protected virtual void OnInjectModel()
    {
    }

    protected virtual void OnRemoveModel()
    {
    }

    public void AddToDisposables(IDisposable disposable)
    {
        disposables.Add(disposable);
    }

    private void OnDestroy()
    {
        RemoveModel();
    }

    public void Dispose()
    {
        DestroyImmediate(this.gameObject);
    }
}