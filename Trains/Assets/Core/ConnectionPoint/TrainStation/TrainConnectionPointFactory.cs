using UnityEngine;

public class TrainConnectionPointFactory : MonoBehaviour
{
    [SerializeField]
    ConnectionPointMode modeManager;

    [ContextMenu(nameof(Create))]
    public void Create()
    {
        var temp = new TrainStation();
        modeManager.Create(temp);
    }
}
