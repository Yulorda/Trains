using System;
using UnityEngine;
using Zenject;

public class RoadPresentersFactory : MonoBehaviour, IFactory<Road>
{
    [SerializeField]
    RoadLRPresenterFactory LRPresenterFactory;

    [Inject]
    private void RegistrateFactory(Manager manager)
    {
        manager.RegistrateFactory(this);
    }

    public IDisposable Create(Road connection)
    {
        return LRPresenterFactory.Create(connection);
    }
}