using System;
using UnityEngine;

public class TrainPresentersFactory : MonoBehaviour, IFactory<Train>
{
    [SerializeField]
    TrainMovingPlatformPresenterFactory movingPlatformFactory;

    private void Awake()
    {
        Manager.GetInstance().RegistrateFactory(this);
    }

    public IDisposable Create(Train connection)
    {
        return movingPlatformFactory.Create(connection);
    }
}