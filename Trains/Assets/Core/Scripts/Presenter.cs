using System;
using System.Collections.Generic;

public class Presenter<T> : IDisposable
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

    public void Dispose()
    {
        disposables.ForEach(x => x.Dispose());
        disposables.Clear();
    }
}