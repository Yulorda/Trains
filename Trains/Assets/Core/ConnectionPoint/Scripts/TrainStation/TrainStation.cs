using UniRx;
using UnityEngine;

public class TrainStation : IConnectionPoint
{
    public ReactiveProperty<Vector3> Position => position;
    private ReactiveProperty<Vector3> position = new ReactiveProperty<Vector3>();
}