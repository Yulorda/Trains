using System;

public class Network
{
    private GenericListeners genericListeners = new GenericListeners();

    public void AddListener<T>(Action<T> action) where T : class
    {
        genericListeners.AddListener(action);
    }

    public void RemoveListener<T>(Action<T> action) where T:class
    {
        genericListeners.RemoveListener(action);
    }
}