using System.Collections.Generic;
using UnityEngine;

public class ConnectorPoint : MonoBehaviour
{
    public enum ConnectorType { Input, Output }
    public ConnectorType Type;

    public BaseComponent OwnerComponent;

    public List<ConnectorPoint> ConnectedPoints = new(); // ✅ Поддержка множественных соединений

    public void ConnectTo(ConnectorPoint other)
    {
        if (other == null || other == this || other.Type == this.Type)
            return;

        // Проверяем, уже ли соединены
        if (ConnectedPoints.Contains(other))
            return;

        // Устанавливаем соединения
        ConnectedPoints.Add(other);
        other.ConnectedPoints.Add(this);

        if (Type == ConnectorType.Output)
            OwnerComponent.ConnectOutput(other.OwnerComponent);
        else
            OwnerComponent.ConnectInput(other.OwnerComponent);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}


