using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class Manager
{
    private DisposableGroup disposableGroup = new DisposableGroup();
    private GenericFunctions genericListeners = new GenericFunctions();
    public ReactiveDictionary<Type, ReactiveCollection<object>> models = new ReactiveDictionary<Type, ReactiveCollection<object>>();

    private static Manager manager;

    private Manager()
    {
    }

    public static Manager GetInstance()
    {
        if (manager == null)
            manager = new Manager();

        return manager;
    }

    public void RegistrateModel<T>(T value)
    {
        var presenters = genericListeners.Invoke(value, typeof(T));
        if (presenters != null)
        {
            disposableGroup.Add(value, presenters);
        }
        models[typeof(T)].Add(value);
    }

    public void RegistrateFactory<T>(IFactory<T> factory) where T : class
    {
        genericListeners.SetFunction<T>(factory.Create);
        models.Add(typeof(T), new ReactiveCollection<object>());
    }

    public void RegistrateOnModelRemove<T>(object obj, Action<T> action) where T : class
    {
        disposableGroup.Add(obj, models[typeof(T)].ObserveRemove().Subscribe(x => action(x.Value as T)));
    }

    public void Clear<T>()
    {
        foreach (var model in models[typeof(T)])
        {
            disposableGroup.Dispose(model);
        }

        models[typeof(T)].Clear();
    }

    public void Remove<T>(T value)
    {
        models[typeof(T)].Remove(value);
        disposableGroup.Dispose(value);
    }
}