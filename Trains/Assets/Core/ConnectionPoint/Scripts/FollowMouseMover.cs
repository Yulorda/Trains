using System.Collections;
using UnityEngine;

public class FollowMouseMover : MonoBehaviour
{
    private IConnectionPoint connectionPoint;

    public void Inject(IConnectionPoint connector)
    {
        this.connectionPoint = connector;
        StartFollow();
    }

    public void StartFollow()
    {
        StartCoroutine(StartFollowing());
    }

    private IEnumerator StartFollowing()
    {
        while (true)
        {
            yield return null;
            Following();

            if (Input.GetMouseButtonDown(0))
            {
                connectionPoint = null;
                break;
            }
        }
    }

    private void Following()
    {
        connectionPoint.Position.SetValueAndForceNotify(GetPosition());
    }

    private Vector3 GetPosition()
    {
        var position = connectionPoint.Position.Value;
        Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(position);
        Vector3 positionResult = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen.z));
        return new Vector3(positionResult.x, position.y, positionResult.z);
    }
}