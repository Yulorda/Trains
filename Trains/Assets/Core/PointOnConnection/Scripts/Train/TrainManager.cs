using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrainManager : MonoBehaviour
{
    [Inject]
    Manager manager;

    private void Start()
    {
        manager.RegistrateOnModelRemove<Road>(this, OnConnectionRemove);
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
                        var connectorPresenter = hit.collider.gameObject.GetComponent<TrainMovingPlatformPresenter>();
                        if (connectorPresenter != null)
                        {
                            manager.Remove(connectorPresenter.model);
                        }
                    }
                }
                yield break;
            }
        }
    }

    protected IEnumerator CreateCoroutine()
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

                        var result = connectorPresenter?.model;
                        if (result != null)
                        {
                            Create(result, hit);
                        }
                    }
                }
                yield break;
            }
        }
    }

    public void Create(IConnection connection, RaycastHit hit)
    {
        var start = connection.PointStart.Value.Position.Value;
        var end = connection.PointEnd.Value.Position.Value;
        Train train = new Train(connection, MathExtension.GetPointOnLine(start, end, hit.point));
        manager.RegistrateModel(train);
    }

    private void OnConnectionRemove(Road road)
    {
        List<Train> removeList = new List<Train>();

        foreach (Train train in manager.models[typeof(Train)])
        {
            if (train.Connection.Value == road)
            {
                removeList.Add(train);
            }
        }

        foreach (var train in removeList)
        {
            manager.Remove(train);
        }
    }
}