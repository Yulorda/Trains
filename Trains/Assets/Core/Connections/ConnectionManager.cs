using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField]
    private ConnectionLinePresenterFactory connectionPresenterFactory;

    [SerializeField]
    private ConnectionPointManager connectorModeManager;

    [HideInInspector]
    public ReactiveCollection<IConnection> connections = new ReactiveCollection<IConnection>();

    private DisposableGroup disposableGroup = new DisposableGroup();

    private void Awake()
    {
        disposableGroup.Add(connectorModeManager, connectorModeManager.connectionPoints.ObserveRemove().Subscribe(x => OnRemoveConnectionPoint(x.Value)));    
    }

    public void Create(IConnection connection)
    {
        if (ValidateConnection(connection))
        {
            var presenter = connectionPresenterFactory.Create(connection);
            disposableGroup.Add(connection, presenter);
            connections.Add(connection);
        }
    }

    public void Clear()
    {
        connections.Clear();
        disposableGroup.Dispose();
    }

    public void Remove(IConnection connection)
    {
        connections.Remove(connection);
        disposableGroup.Dispose(connection);
    }

    private void OnRemoveConnectionPoint(IConnectionPoint connectionPoint)
    {
        List<IConnection> removeList = new List<IConnection>();

        foreach(var connection in connections)
        {
            if(connectionPoint == connection.PointStart.Value || connectionPoint == connection.PointEnd.Value)
            {
                removeList.Add(connection);
            }
        }

        foreach(var remove in removeList)
        {
            connections.Remove(remove);
            disposableGroup.Dispose(remove);
        }
    }

    private bool ValidateConnection(IConnection connection)
    {
        if (connection.PointStart.Value != connection.PointEnd.Value)
        {
            foreach (var save in connections)
            {
                if (save.PointStart.Value != connection.PointEnd.Value && save.PointStart.Value != connection.PointStart.Value)
                {
                    continue;
                }
                else if (save.PointEnd.Value != connection.PointEnd.Value && save.PointEnd.Value != connection.PointStart.Value)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}