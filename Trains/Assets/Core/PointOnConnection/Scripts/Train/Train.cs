using System;
using System.Runtime.Serialization;
using System.Transactions;
using UniRx;

[Serializable]
public class Train : IPointOnConnection
{
    public ReactiveProperty<IConnection> Connection => connection;
    public ReactiveProperty<float> Position => position;

    [NonSerialized]
    private ReactiveProperty<IConnection> connection = new ReactiveProperty<IConnection>();
    [NonSerialized]
    private ReactiveProperty<float> position = new ReactiveProperty<float>();

    public Train(IConnection connection, float position)
    {
        this.connection.Value = connection;
        this.position.Value = position;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("position", position.Value);
    }

    private Train(SerializationInfo info, StreamingContext context)
    {
        position.Value = info.GetSingle("position");
    }
}