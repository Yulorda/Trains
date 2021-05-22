using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PointOnConnectionManager : MonoBehaviour
{
    [SerializeField]
    private TrainPresenterFactory presenterFactory;

    [SerializeField]
    private ConnectionManager connectionManager;

    [HideInInspector]
    public ReactiveCollection<IPointOnConnection> pointsOnConnection = new ReactiveCollection<IPointOnConnection>();

    private DisposableGroup disposableGroup = new DisposableGroup();

    private void Awake()
    {
        disposableGroup.Add(connectionManager, connectionManager.connections.ObserveRemove().Subscribe(x => OnRemoveConnectionPoint(x.Value)));
    }

    public void Create(IPointOnConnection pointOnConnection)
    {
        var presenter = presenterFactory.Create(pointOnConnection);
        disposableGroup.Add(pointOnConnection, presenter);
        pointsOnConnection.Add(pointOnConnection);
    }

    public void Clear()
    {
        pointsOnConnection.Clear();
        disposableGroup.Dispose();
    }

    public void Remove(IPointOnConnection connection)
    {
        pointsOnConnection.Remove(connection);
        disposableGroup.Dispose(connection);
    }

    private void OnRemoveConnectionPoint(IConnection connection)
    {
        List<IPointOnConnection> removeList = new List<IPointOnConnection>();

        foreach (var pointOnConnection in pointsOnConnection)
        {
            if (pointOnConnection.Connection.Value == connection)
            {
                removeList.Add(pointOnConnection);
            }
        }

        foreach (var remove in removeList)
        {
            pointsOnConnection.Remove(remove);
            disposableGroup.Dispose(remove);
        }
    }
}