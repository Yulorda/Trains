using System.Collections;
using UnityEngine;
using Zenject;

public class TrainStationManager : MonoBehaviour
{
    [SerializeField]
    private FollowMouseMover followMouseMover;
    [Inject]
    private Manager manager;

    public virtual void Create()
    {
        StopAllCoroutines();
        var train = new TrainStation();
        followMouseMover.Inject(train);
        manager.RegistrateModel(train);
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

    private IEnumerator DeleteCoroutine()
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
                        var connectorPresenter = hit.collider.gameObject.GetComponent<TrainStationMovingPlatformPresenter>();
                        if (connectorPresenter?.model != null)
                        {
                            manager.Remove(connectorPresenter?.model);
                        }
                    }
                }

                yield break;
            }
        }
    }
}