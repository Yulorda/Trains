using System;
using UnityEngine;

public class TrainStationPresentersFactory : MonoBehaviour, IFactory<TrainStation>
{
    [SerializeField]
    TrainStationMovingPlatformPresenterFactory movingPlatformFactory;

    private void Awake()
    {
        Manager.GetInstance().RegistrateFactory(this);
    }

    public IDisposable Create(TrainStation connection)
    {
        return movingPlatformFactory.Create(connection);
    }
}