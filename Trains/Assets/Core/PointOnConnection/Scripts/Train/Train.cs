using UniRx;

public class Train : IPointOnConnection
{
    public ReactiveProperty<IConnection> Connection => connection;
    public ReactiveProperty<float> Position => position;

    private ReactiveProperty<IConnection> connection = new ReactiveProperty<IConnection>();
    private ReactiveProperty<float> position = new ReactiveProperty<float>();

    public Train(IConnection connection, float position)
    {
        this.connection.Value = connection;
        this.position.Value = position;
    }
}