using System.Collections;
using UnityEngine;

public class LineRendererConnectionPresenter : PresenterBehaviour<ConnectionManager>
{
    private IConnectionPoint pointStart;
    private IConnectionPoint pointEnd;

    public void Create()
    {
        StopAllCoroutines();
        pointStart = null;
        pointEnd = null;
        StartCoroutine(FindPoint());
    }

    public void Delete()
    {
        StopAllCoroutines();
        StartCoroutine(DeleteConnection());
    }

    public void Return()
    {
        StopAllCoroutines();
    }

    private IEnumerator DeleteConnection()
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
                        var connectorPresenter = hit.collider.gameObject.GetComponentInParent<ConnectionLinePresenter>();
                        if(connectorPresenter!=null)
                        {
                            model.Remove(connectorPresenter.model);
                        }
                    }
                }

                yield break;
            }
        }
    }

    private IEnumerator FindPoint()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                if (pointStart == null)
                {
                    pointStart = GetPoint();
                    continue;
                }
                else
                {
                    pointEnd = GetPoint();
                    if (pointEnd != null)
                    {
                        model.Create(new Road(pointStart, pointEnd));
                        break;
                    }
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
                var connectorPresenter = hit.collider.gameObject.GetComponent<ConnectionPointPresenter>();
                return connectorPresenter?.model;
            }
        }
        return null;
    }
}