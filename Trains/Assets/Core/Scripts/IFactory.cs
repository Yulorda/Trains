using System;

public interface IFactory<T>
{
    IDisposable Create(T connection);
}
