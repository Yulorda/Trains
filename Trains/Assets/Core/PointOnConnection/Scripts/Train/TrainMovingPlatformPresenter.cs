using UniRx;
using UnityEngine;

public class TrainMovingPlatformPresenter : PresenterBehaviour<Train>
{
    [SerializeField]
    private EmissionHighlighter highlighter;

    private Vector3 PointStart
    {
        get
        {
            return model.Connection.Value.PointStart.Value.Position.Value;
        }
    }

    private Vector3 PointEnd
    {
        get
        {
            return model.Connection.Value.PointEnd.Value.Position.Value;
        }
    }

    protected override void OnInjectModel()
    {
        base.OnInjectModel();
        AddToDisposables(model.Position.Subscribe(x => SetPostion(x)));
        AddToDisposables(model.Connection.Value.PointStart.Value.Position.Subscribe(x => OnConnectionChangePosition()));
        AddToDisposables(model.Connection.Value.PointEnd.Value.Position.Subscribe(x => OnConnectionChangePosition()));
    }

    private void SetPostion(float value)
    {
        transform.position = MathExtension.GetPointOnLine(PointStart, PointEnd, value);
    }

    private void OnConnectionChangePosition()
    {
        transform.position = MathExtension.GetPointOnLine(PointStart, PointEnd, model.Position.Value);
    }

    private void OnMouseUp()
    {
        highlighter.Highlight(false);
    }

    private void OnMouseDown()
    {
        highlighter.Highlight(true);
    }

    private void OnMouseDrag()
    {
        Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen.z));
        model.Position.Value = MathExtension.GetPointOnLine(PointStart, PointEnd, point);
    }
}