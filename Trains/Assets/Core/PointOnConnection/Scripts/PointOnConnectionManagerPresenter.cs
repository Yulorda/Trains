using System.Collections;
using UnityEngine;

public class PointOnConnectionManagerPresenter : PresenterBehaviour<PointOnConnectionManager>
{
    public void Create()
    {
        StopAllCoroutines();
        StartCoroutine(FindLineToCreate());
    }

    public void Delete()
    {
        StopAllCoroutines();
        StartCoroutine(DeletePointToConnection());
    }

    public void Return()
    {
        StopAllCoroutines();
    }

    private IEnumerator DeletePointToConnection()
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
                        var connectorPresenter = hit.collider.gameObject.GetComponent<PointOnConnectionPresenter>();
                        if (connectorPresenter != null)
                        {
                            model.Remove(connectorPresenter.model);
                        }
                    }
                }

                yield break;
            }
        }
    }

    private IEnumerator FindLineToCreate()
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
                        var connectorPresenter = hit.collider.gameObject.GetComponentInParent<PresenterBehaviour<IConnection>>();

                        var result = connectorPresenter?.model;
                        if (result != null)
                        {
                            IPointOnConnection train = new Train();
                            train.Connection.Value = result;
                            train.Position.Value = GetPointOnLine(result, hit.point);
                            model.Create(train);
                        }
                    }
                }
                yield break;
            }
        }
    }

    private float GetPointOnLine(IConnection connection, Vector3 point)
    {
        var start = connection.PointStart.Value.Position.Value;
        var end = connection.PointEnd.Value.Position.Value;

        var line = end - start;
        Vector3 pToStart = point - start;
        return Mathf.Clamp(Vector3.Dot(pToStart, line.normalized) / line.magnitude, 0, 1);
    }
}