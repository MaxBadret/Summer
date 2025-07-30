using System.Collections.Generic;
using UnityEngine;

public class ConnectorPoint : MonoBehaviour
{
    

     [SerializeField] public BaseComponent OwnerComponent;

    public List<ConnectorPoint> ConnectedPoints = new(); // ✅ Поддержка множественных соединений

    public void ConnectTo(ConnectorPoint other)
    {
        if (other == null || other == this)
            return;

        // Проверяем, уже ли соединены
        if (ConnectedPoints.Contains(other))
            return;

        // Устанавливаем соединения
        ConnectedPoints.Add(other);
        other.ConnectedPoints.Add(this);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}


