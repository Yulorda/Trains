using UniRx;

public class Road : IConnection
{
    ReactiveProperty<IConnectionPoint> IConnection.PointStart => pointStart;
    ReactiveProperty<IConnectionPoint> IConnection.PointEnd => pointEnd;

    private ReactiveProperty<IConnectionPoint> pointStart = new ReactiveProperty<IConnectionPoint>();
    private ReactiveProperty<IConnectionPoint> pointEnd = new ReactiveProperty<IConnectionPoint>();

    public Road(IConnectionPoint pointStart, IConnectionPoint pointEnd)
    {
        this.pointStart.Value = pointStart;
        this.pointEnd.Value = pointEnd;
    }
}