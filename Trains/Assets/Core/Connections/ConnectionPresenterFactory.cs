using UnityEngine;

public class ConnectionPresenterFactory : MonoBehaviour
{
    private DisposableGroup presenters = new DisposableGroup();

    [SerializeField]
    private ConnectionLinePresenterFactory linePresenterFactory;

    public void Create(Connection connection)
    {
        presenters.Add(connection, linePresenterFactory.Create(connection));
    }

    public void Remove(Connection connection)
    {
        presenters.Dispose(connection);
    }
}