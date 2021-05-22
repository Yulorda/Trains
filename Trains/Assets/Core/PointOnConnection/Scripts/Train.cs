using UniRx;

public class Train : IPointOnConnection
{
    ReactiveProperty<IConnection> IPointOnConnection.Connection => connection;
    ReactiveProperty<float> IPointOnConnection.Position => position;

    private ReactiveProperty<IConnection> connection = new ReactiveProperty<IConnection>();
    private ReactiveProperty<float> position = new ReactiveProperty<float>();
}