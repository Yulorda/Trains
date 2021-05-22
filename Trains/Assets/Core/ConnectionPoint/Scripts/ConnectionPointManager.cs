using UniRx;
using UnityEngine;

public class ConnectionPointManager : MonoBehaviour
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

    public void Remove(IConnectionPoint connectionPoint)
    {
        connectionPoints.Remove(connectionPoint);
        disposableGroup.Dispose(connectionPoint);
    }

    public void Clear()
    {
        disposableGroup.Dispose();
        connectionPoints.Clear();
    }
}
