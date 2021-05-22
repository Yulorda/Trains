using UniRx;
using UnityEngine;

public class TrainStation : IConnectionPoint
{
    ReactiveProperty<Vector3> IConnectionPoint.Position => position;
    private ReactiveProperty<Vector3> position = new ReactiveProperty<Vector3>();
}