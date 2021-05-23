using System.Runtime.Serialization;
using UniRx;

public interface IPointOnConnection : ISerializable
{
    ReactiveProperty<IConnection> Connection { get; }
    ReactiveProperty<float> Position { get; }
}
