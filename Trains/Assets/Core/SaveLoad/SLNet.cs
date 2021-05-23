using UnityEngine;
using Zenject;

public class SLNet : MonoBehaviour
{
    [Inject]
    SaveLoadSystem saveLoadSystem;
    [Inject]
    Network network;

    private void Start()
    {
        network.AddListener<SaveSnapshot>(saveLoadSystem.Load);   
    }
}
