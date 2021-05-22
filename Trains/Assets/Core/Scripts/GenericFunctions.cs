using System;
using System.Collections.Generic;

public class GenericFunctions
{
    private interface IGenericFunction
    {
        IDisposable Invoke(object obj);
    }

    private class GenericFunction<T> : IGenericFunction where T : class
    {
        public Func<T,IDisposable> function;

        public void SetFunction(Func<T,IDisposable> listener)
        {
            function = listener;
        }

        public void RemoveFunction(Func<T, IDisposable> listener)
        {
            function = null;
        }

        public IDisposable Invoke(object obj)
        {
            return function?.Invoke(obj as T);
        }
    }

    private Dictionary<Type, IGenericFunction> functions = new Dictionary<Type, IGenericFunction>();

    public void SetFunction<T>(Func<T, IDisposable> handler) where T : class
    {
        Type t = typeof(T);
        IGenericFunction listener;

        if (functions.ContainsKey(t))
        {
            listener = functions[t];
        }
        else
        {
            listener = new GenericFunction<T>();
            functions.Add(t, listener);
        }

        (listener as GenericFunction<T>).SetFunction(handler);
    }

    public void RemoveFunction<T>(Func<T, IDisposable> handler) where T : class
    {
        Type t = typeof(T);
        if (functions.ContainsKey(t))
        {
            (functions[t] as GenericFunction<T>).RemoveFunction(handler);
        }
    }

    public void RemoveAllFunctions()
    {
        functions.Clear();
    }

    public IDisposable Invoke<T>(T obj)
    {
        return Invoke(obj, typeof(T));
    }

    public IDisposable Invoke(object obj, Type t)
    {
        if (functions.TryGetValue(t, out IGenericFunction function))
        {
            return function.Invoke(obj);
        }
        throw new Exception("func not exist");
    }
}

//public class GenericDictionary
//{
//    private interface IGenericListener
//    {
//        void Invoke(object obj);
//    }

//    private class GenericListener<T> : IGenericListener where T : class
//    {
//        public Action<T> listeners;

//        public void AddListener(Action<T> listener)
//        {
//            listeners -= listener;
//            listeners += listener;
//        }

//        public void RemoveListener(Action<T> listener)
//        {
//            listeners -= listener;
//        }

//        public void Invoke(object obj)
//        {
//            listeners?.Invoke(obj as T);
//        }
//    }

//    private Dictionary<Type, IGenericListener> listeners = new Dictionary<Type, IGenericListener>();

//    public void AddListener<T>(Action<T> handler) where T : class
//    {
//        Type t = typeof(T);
//        IGenericListener listener;

//        if (listeners.ContainsKey(t))
//        {
//            listener = listeners[t];
//        }
//        else
//        {
//            listener = new GenericListener<T>();
//            listeners.Add(t, listener);
//        }

//        (listener as GenericListener<T>).AddListener(handler);
//    }

//    public void RemoveListener<T>(Action<T> handler) where T : class
//    {
//        Type t = typeof(T);
//        if (listeners.ContainsKey(t))
//        {
//            (listeners[t] as GenericListener<T>).RemoveListener(handler);
//        }
//    }

//    public void RemoveAllListeners()
//    {
//        listeners.Clear();
//    }

//    public void Invoke<T>(T obj)
//    {
//        Invoke(obj, typeof(T));
//    }

//    public void Invoke(object obj, Type t)
//    {
//        if (listeners.TryGetValue(t, out IGenericListener listener))
//        {
//            listener.Invoke(obj);
//        }
//    }

//    public void InvokeAll(object obj)
//    {
//        foreach (var pair in listeners)
//        {
//            pair.Value?.Invoke(obj);
//        }
//    }
//}