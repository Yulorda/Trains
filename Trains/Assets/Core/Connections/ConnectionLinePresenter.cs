using UniRx;
using UnityEngine;

public class ConnectionLinePresenter : PresenterBehaviour<IConnection>
{
    [SerializeField]
    private LineRenderer lineRenderer;

    protected override void OnInjectModel()
    {
        base.OnInjectModel();

        AddToDisposables(model.PointStart.Value.Position.Subscribe(x => OnConnectionChangePosition(0, x)));
        AddToDisposables(model.PointEnd.Value.Position.Subscribe(x => OnConnectionChangePosition(1, x)));

        DrawLine();
    }

    private void DrawLine()
    {
        lineRenderer.positionCount = 2;
        OnConnectionChangePosition(0, model.PointStart.Value.Position.Value);
        OnConnectionChangePosition(1, model.PointEnd.Value.Position.Value);
    }

    private void OnConnectionChangePosition(int index, Vector3 value)
    {
        lineRenderer.SetPosition(index, value);
    }
}