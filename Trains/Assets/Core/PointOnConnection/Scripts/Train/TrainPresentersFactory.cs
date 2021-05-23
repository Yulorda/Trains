using System;
using UnityEngine;
using Zenject;

public class TrainPresentersFactory : MonoBehaviour, IFactory<Train>
{
    [SerializeField]
    private TrainMovingPlatformPresenterFactory movingPlatformFactory;

    [Inject]
    private void RegistrateFactory(Manager manager)
    {
        manager.RegistrateFactory(this);
    }

    public IDisposable Create(Train connection)
    {
        return movingPlatformFactory.Create(connection);
    }
}