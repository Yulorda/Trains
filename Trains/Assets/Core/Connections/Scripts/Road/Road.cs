using Newtonsoft.Json;
using System;
using UniRx;

public class Road : IConnection
{
    [JsonIgnore]
    public ReactiveProperty<IConnectionPoint> PointStart => pointStart;
    [JsonIgnore]
    public ReactiveProperty<IConnectionPoint> PointEnd => pointEnd;

    [NonSerialized]
    private ReactiveProperty<IConnectionPoint> pointStart = new ReactiveProperty<IConnectionPoint>();

    [NonSerialized]
    private ReactiveProperty<IConnectionPoint> pointEnd = new ReactiveProperty<IConnectionPoint>();

    public Road(IConnectionPoint pointStart, IConnectionPoint pointEnd)
    {
        this.pointStart.Value = pointStart;
        this.pointEnd.Value = pointEnd;
    }
}