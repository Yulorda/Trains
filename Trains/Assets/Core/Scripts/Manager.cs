using System;
using UniRx;

public class Manager
{
    private DisposableGroup disposableGroup = new DisposableGroup();
    private GenericFunctions genericFunctions = new GenericFunctions();
    public ReactiveDictionary<Type, ReactiveCollection<object>> models = new ReactiveDictionary<Type, ReactiveCollection<object>>();

    private Manager()
    {

    }

    public void RegistrateModel<T>(T model)
    {
        var presenters = genericFunctions.Invoke(model, typeof(T));
        if (presenters != null)
        {
            disposableGroup.Add(model, presenters);
        }
        models[typeof(T)].Add(model);
    }

    public void RegistrateModel(object model, Type t)
    {
        var presenters = genericFunctions.Invoke(model, t);
        if (presenters != null)
        {
            disposableGroup.Add(model, presenters);
        }
        models[t].Add(model);
    }

    public void RegistrateFactory<T>(IFactory<T> factory) where T : class
    {
        genericFunctions.SetFunction<T>(factory.Create);
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

    public void Remove(object value, Type t)
    {
        models[t].Remove(value);
        disposableGroup.Dispose(value);
    }
}