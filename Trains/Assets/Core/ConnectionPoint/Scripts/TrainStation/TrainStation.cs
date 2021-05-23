using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using UniRx;
using UnityEngine;

[Serializable]
public class TrainStation : IConnectionPoint
{
    public ReactiveProperty<Vector3> Position => position;
    [NonSerialized]
    private ReactiveProperty<Vector3> position = new ReactiveProperty<Vector3>();

    public TrainStation()
    {
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("X", position.Value.x);
        info.AddValue("Y", position.Value.y);
        info.AddValue("Z", position.Value.z);
    }

    private TrainStation(SerializationInfo info, StreamingContext context)
    {
        position.Value = new Vector3(info.GetSingle("X"), info.GetSingle("Y"), info.GetSingle("Z"));
    }
}