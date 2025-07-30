using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance { get; private set; }

    private ConnectorPoint selectedPoint;

    public GameObject wirePrefab;

    // 🔧 Храним все соединения как пары
    private readonly HashSet<(ConnectorPoint, ConnectorPoint)> connections = new();

    private bool canCreateWire = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canCreateWire)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

            if (hit.collider != null)
            {
                ConnectorPoint point = hit.collider.GetComponent<ConnectorPoint>();
                if (point != null)
                {
                    HandleConnectorClick(point);
                }
            }
        }

        /*if (Input.GetKey(KeyCode.Space))
        {
            foreach (var connection in connections)
            {
                Debug.Log(connection.Item1, connection.Item2);
            }
        }*/
    }

    private void HandleConnectorClick(ConnectorPoint point)
    {
        if (selectedPoint == null)
        {
            selectedPoint = point;
        }
        else
        {
            if (selectedPoint != point)
            {
                ConnectorPoint from = selectedPoint;
                ConnectorPoint to = point;

                if (ConnectionExists(from, to))
                {
                    Debug.LogWarning("Connection already exists!");
                }
                else
                {
                    from.ConnectTo(to);
                    CreateWire(from, to);
                    RegisterConnection(from, to);
                }
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

    // 🔒 Проверка и регистрация соединений
    private bool ConnectionExists(ConnectorPoint a, ConnectorPoint b)
    {
        return connections.Contains((a, b)) || connections.Contains((b, a));
    }
    
    private void RegisterConnection(ConnectorPoint a, ConnectorPoint b)
    {
        // if (a.Type == ConnectorPoint.ConnectorType.Output)
        // {
        //     a.OwnerComponent.ConnectOutput(b.OwnerComponent);
        //     b.OwnerComponent.ConnectInput(a.OwnerComponent);
        // }
        // else
        // {
        //     a.OwnerComponent.ConnectInput(b.OwnerComponent);
        //     b.OwnerComponent.ConnectOutput(a.OwnerComponent);
        // }
        connections.Add((a, b));

        CircuitManager.Instance.MarkCircuitChanged();
    }
    
    public void RemoveConnection(ConnectorPoint a, ConnectorPoint b)
    {
        connections.Remove((a, b));
        connections.Remove((b, a));

        CircuitManager.Instance.MarkCircuitChanged();
    }
    
    // public void WireModeSwitch()
    // {
    //     canCreateWire = !canCreateWire;
    // }
    
    public void RemoveAllConnectionsWith(BaseComponent component)
    {
        List<(ConnectorPoint, ConnectorPoint)> toRemove = new();

        foreach (var pair in connections)
        {
            if (pair.Item1.OwnerComponent == component || pair.Item2.OwnerComponent == component)
            {
                toRemove.Add(pair);
            }
        }

        foreach (var pair in toRemove)
        {
            // Удаление визуального провода
            Wire[] allWires = Object.FindObjectsByType<Wire>(FindObjectsSortMode.None);

            foreach (var wire in allWires)
            {
                if ((wire.from == pair.Item1 && wire.to == pair.Item2) ||
                    (wire.from == pair.Item2 && wire.to == pair.Item1))
                {
                    wire.from.ConnectedPoints.Remove(wire.to);
                    wire.to.ConnectedPoints.Remove(wire.from);
                    Destroy(wire.gameObject);
                }
            }

            connections.Remove(pair);
        }

        CircuitManager.Instance.MarkCircuitChanged();
    }
}

