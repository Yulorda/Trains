using UniRx;

public class Connection : ISelectable, IConnection
{
    public ReactiveProperty<bool> IsSelected { get; private set; } = new ReactiveProperty<bool>();
    public ReactiveProperty<IConnectionPoint> PointStart { get; set; } = new ReactiveProperty<IConnectionPoint>();
    public ReactiveProperty<IConnectionPoint> PointEnd { get; set; } = new ReactiveProperty<IConnectionPoint>();

    public Connection(IConnectionPoint pointStart, IConnectionPoint pointEnd)
    {
        this.PointStart.Value = pointStart;
        this.PointEnd.Value = pointEnd;
    }

    public void Select()
    {
        IsSelected.SetValueAndForceNotify(true);
    }

    public void UnSelect()
    {
        IsSelected.SetValueAndForceNotify(false);
    }
}