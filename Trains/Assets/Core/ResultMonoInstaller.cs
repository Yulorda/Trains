using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResultMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Manager>().AsSingle().NonLazy();
        Container.Bind<SaveLoadSystem>().AsSingle().NonLazy();
        Container.Bind<JSONSerializator>().AsSingle().NonLazy();
        Container.Bind<Network>().AsSingle().NonLazy();
    }
}
