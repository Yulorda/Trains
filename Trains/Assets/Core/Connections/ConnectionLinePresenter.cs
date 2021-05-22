using UniRx;
using UnityEngine;

public class ConnectionLinePresenter : PresenterBehaviour<IConnection>
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform boxColliderTransform;
    [SerializeField]
    private BoxCollider boxCollider;

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
        ChangeColliderPosition();
    }

    private void OnConnectionChangePosition(int index, Vector3 value)
    {
        lineRenderer.SetPosition(index, value);
        ChangeColliderPosition();
    }

    private void ChangeColliderPosition()
    {
        var startPos = lineRenderer.GetPosition(0);
        var endPos = lineRenderer.GetPosition(1);
        float lineLength = Vector3.Distance(startPos, endPos); 
        boxCollider.size = new Vector3(lineRenderer.startWidth, lineRenderer.startWidth, lineLength); 
        Vector3 midPoint = (startPos + endPos) / 2;
        boxColliderTransform.position = midPoint;
        boxColliderTransform.LookAt(startPos);
    }
}