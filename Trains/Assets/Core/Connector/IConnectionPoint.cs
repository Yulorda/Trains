using UniRx;
using UnityEngine;

public interface IConnectionPoint
{
    ReactiveProperty<Vector3> Position { get; }
}