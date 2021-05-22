using System.Collections;
using UnityEngine;

public class ConnectionCreator : MonoBehaviour
{
    [SerializeField]
    private ConnectionModeManager manager;

    private IConnectionPoint pointStart;
    private IConnectionPoint pointEnd;

    public void Create()
    {
        pointStart = null;
        pointEnd = null;
        StartCoroutine(FindPoint());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator FindPoint()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (Input.GetMouseButtonUp(0))
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
                        manager.Create(new Road(pointStart, pointEnd));
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