using System.Runtime.Serialization;
using UniRx;

public interface IConnection
{
    ReactiveProperty<IConnectionPoint> PointStart { get; }
    ReactiveProperty<IConnectionPoint> PointEnd { get; }
}