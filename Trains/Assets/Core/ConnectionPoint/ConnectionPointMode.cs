using UniRx;
using UnityEngine;

public class ConnectionPointMode : MonoBehaviour
{
    [SerializeField]
    ConnectionPointPresenterFactory pointPresenterFactory;
    [SerializeField]
    FollowMouseMover followMouseMover;

    [HideInInspector]
    public ReactiveCollection<IConnectionPoint> connectionPoints = new ReactiveCollection<IConnectionPoint>();

    private DisposableGroup disposableGroup = new DisposableGroup();

    public void Create(IConnectionPoint connectionPoint)
    {
        var presenter = pointPresenterFactory.Create(connectionPoint);
        connectionPoint.Position.SetValueAndForceNotify(presenter.transform.position);
        disposableGroup.Add(connectionPoint, presenter);
        connectionPoints.Add(connectionPoint);
        followMouseMover.Inject(connectionPoint);
    }

    public void Clear()
    {
        disposableGroup.Dispose();
    }
}
