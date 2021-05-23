using UniRx;
using UnityEngine;

public class TrainStationMovingPlatformPresenter : PresenterBehaviour<TrainStation>
{
    [SerializeField]
    private EmissionHighlighter highlighter;

    protected override void OnInjectModel()
    {
        base.OnInjectModel();
        AddToDisposables(model.Position.Subscribe(x => transform.position = x));
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
        var position = model.Position.Value;
        Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(position);
        Vector3 positionResult = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen.z));
        model.Position.SetValueAndForceNotify(new Vector3(positionResult.x, position.y, positionResult.z));
    }
}
