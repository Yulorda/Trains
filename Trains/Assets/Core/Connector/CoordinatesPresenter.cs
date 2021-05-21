using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CoordinatesPresenter : PresenterBehaviour<TrainStation>
{
    [SerializeField]
    private Text coordinates;

    protected override void OnInjectModel()
    {
        base.OnInjectModel();
        AddToDisposables(model.Position.Subscribe(x => OnMoving(model)));
    }

    private void OnMoving(TrainStation movingConnector)
    {
        coordinates.text = movingConnector.Position.Value.ToString();
    }
}