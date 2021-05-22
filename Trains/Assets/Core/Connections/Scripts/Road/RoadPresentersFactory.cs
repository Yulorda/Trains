using System;
using UnityEngine;

public class RoadPresentersFactory : MonoBehaviour, IFactory<Road>
{
    [SerializeField]
    RoadLRPresenterFactory LRPresenterFactory;

    private void Awake()
    {
        Manager.GetInstance().RegistrateFactory(this);
    }

    public IDisposable Create(Road connection)
    {
        return LRPresenterFactory.Create(connection);
    }
}