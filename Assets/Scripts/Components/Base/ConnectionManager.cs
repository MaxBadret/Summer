using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance { get; private set; }

    private ConnectorPoint selectedPoint;

    public GameObject wirePrefab; // Префаб с LineRenderer + Wire.cs

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnConnectorClicked(ConnectorPoint point)
    {
        if (selectedPoint == null)
        {
            selectedPoint = point;
        }
        else
        {
            // Соединение противоположных типов
            if (selectedPoint.Type != point.Type)
            {
                ConnectorPoint from = selectedPoint.Type == ConnectorPoint.ConnectorType.Output ? selectedPoint : point;
                ConnectorPoint to = selectedPoint.Type == ConnectorPoint.ConnectorType.Output ? point : selectedPoint;

                from.ConnectTo(to);
                CreateWire(from, to);
            }

            selectedPoint = null;
        }
    }

    private void CreateWire(ConnectorPoint from, ConnectorPoint to)
    {
        GameObject wireObj = Instantiate(wirePrefab);
        Wire wire = wireObj.GetComponent<Wire>();
        wire.from = from;
        wire.to = to;
    }
}

