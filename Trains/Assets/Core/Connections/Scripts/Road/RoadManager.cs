using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    private void Start()
    {
        Manager.GetInstance().RegistrateOnModelRemove<TrainStation>(this, OnRemoveConnectionPoint);
    }

    public virtual void Create()
    {
        StopAllCoroutines();
        StartCoroutine(CreateCoroutine());
    }

    public virtual void Delete()
    {
        StopAllCoroutines();
        StartCoroutine(DeleteCoroutine());
    }

    public virtual void Return()
    {
        StopAllCoroutines();
    }

    protected IEnumerator DeleteCoroutine()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        var connectorPresenter = hit.collider.gameObject.GetComponentInParent<RoadLRPresenter>();
                        if (connectorPresenter != null)
                        {
                            Manager.GetInstance().Remove(connectorPresenter.model);
                        }
                    }
                }

                yield break;
            }
        }
    }

    protected IEnumerator CreateCoroutine()
    {
        IConnectionPoint pointStart = null;
        IConnectionPoint pointEnd = null;

        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                if (pointStart == null)
                {
                    pointStart = GetPoint();
                    if (pointStart == null)
                    {
                        break;
                    }
                    continue;
                }
                else
                {
                    pointEnd = GetPoint();
                    if (pointEnd != null)
                    {
                        var connection = new Road(pointStart, pointEnd);
                        if (ValidateConnection(connection))
                        {
                            Manager.GetInstance().RegistrateModel(connection);
                        }
                    }
                    break;
                }
            }
        }
    }

    private IConnectionPoint GetPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var connectorPresenter = hit.collider.gameObject.GetComponent<TrainStationMovingPlatformPresenter>();
                return connectorPresenter?.model;
            }
        }
        return null;
    }

    private void OnRemoveConnectionPoint(TrainStation connectionPoint)
    {
        List<Road> removeList = new List<Road>();

        foreach (Road connection in Manager.GetInstance().models[typeof(Road)])
        {
            if (connectionPoint == connection.PointStart.Value || connectionPoint == connection.PointEnd.Value)
            {
                removeList.Add(connection);
            }
        }

        foreach(var road in removeList)
        {
            Manager.GetInstance().Remove(road);
        }
    }

    private bool ValidateConnection(IConnection connection)
    {
        if (connection.PointStart.Value != connection.PointEnd.Value)
        {
            foreach (Road save in Manager.GetInstance().models[typeof(Road)])
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