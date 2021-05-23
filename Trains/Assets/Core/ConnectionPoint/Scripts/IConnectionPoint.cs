using System;
using System.Runtime.Serialization;
using UniRx;
using UnityEngine;

public interface IConnectionPoint: ISerializable
{
    ReactiveProperty<Vector3> Position { get; }
}