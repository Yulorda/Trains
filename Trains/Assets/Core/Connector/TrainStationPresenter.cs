using System.Reactive.Linq;
using UniRx;
using UnityEngine;

public class TrainStationPresenter : PresenterBehaviour<TrainStation>
{
    [SerializeField]
    private EmissionHighlighter highlighter;

    private Vector3 distance;

    protected override void OnInjectModel()
    {
        base.OnInjectModel();
        AddToDisposables(model.Position.Subscribe(SetPostition));
        AddToDisposables(model.IsSelected.Subscribe(OnSelectModel));
    }

    private void SetPostition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnSelectModel(bool value)
    {
        highlighter.Highlight(value);
    }

    private void OnMouseUp()
    {
        model.OnMouseUp.Execute();
    }

    private void OnMouseDown()
    {
        model.OnMouseDown.Execute();
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(model.Position.Value).z)) - model.Position.Value;
    }

    private void OnMouseDrag()
    {
        Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(model.Position.Value);
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
        model.Position.SetValueAndForceNotify(new Vector3(pos_move.x - distance.x, model.Position.Value.y, pos_move.z - distance.z));
    }
}