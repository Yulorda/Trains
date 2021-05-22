using UniRx;

public interface IPointOnConnection
{
    ReactiveProperty<IConnection> Connection { get; }
    ReactiveProperty<float> Position { get; }
}