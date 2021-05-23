using System;
using UnityEngine;
using Zenject;

public class TrainStationPresentersFactory : MonoBehaviour, IFactory<TrainStation>
{
    [SerializeField]
    TrainStationMovingPlatformPresenterFactory movingPlatformFactory;

    [Inject]
    private void RegistrateFactory(Manager manager)
    {
        manager.RegistrateFactory(this);
    }

    public IDisposable Create(TrainStation connection)
    {
        return movingPlatformFactory.Create(connection);
    }
}