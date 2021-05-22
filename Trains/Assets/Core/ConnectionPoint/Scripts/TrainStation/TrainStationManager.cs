using System.Collections;
using UniRx;
using UnityEngine;

public class TrainStationManager : MonoBehaviour
{
    [SerializeField]
    FollowMouseMover followMouseMover;

    public virtual void Create()
    {
        StopAllCoroutines();
        var train = new TrainStation();
        followMouseMover.Inject(train);
        Manager.GetInstance().RegistrateModel(train);
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
                            Manager.GetInstance().Remove(connectorPresenter?.model);
                        }
                    }
                }

                yield break;
            }
        }
    }
}
