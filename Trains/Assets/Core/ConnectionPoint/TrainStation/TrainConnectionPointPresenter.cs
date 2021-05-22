using System.Collections;
using UnityEngine;

public class TrainConnectionPointPresenter : PresenterBehaviour<ConnectionPointManager>
{
    //Zenject ??

    public void Create()
    {
        var temp = new TrainStation();
        model.Create(temp);
    }

    public void Delete()
    {
        StartCoroutine(DeleteCoroutine());
    }

    public void Return()
    {
        StopAllCoroutines();
    }

    private IEnumerator DeleteCoroutine()
    {
        while(true)
        {
            yield return null;

            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        var connectorPresenter = hit.collider.gameObject.GetComponent<ConnectionPointPresenter>();
                        if (connectorPresenter?.model.GetType() == typeof(TrainStation))
                        {
                            model.Remove(connectorPresenter.model);
                        }
                    }
                }

                yield break;
            }
        }
    }
}
