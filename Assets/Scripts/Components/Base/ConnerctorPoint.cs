using UnityEngine;

public class ConnectorPoint : MonoBehaviour
{
    public enum ConnectorType { Input, Output }
    public ConnectorType Type;

    public BaseComponent OwnerComponent; // назначается в BaseComponent
    public ConnectorPoint ConnectedTo;

    private void OnMouseDown()
    {
        ConnectionManager.Instance.OnConnectorClicked(this);
    }

    public void ConnectTo(ConnectorPoint other)
    {
        if (other == null || other == this || other.Type == this.Type)
            return;

        ConnectedTo = other;
        other.ConnectedTo = this;

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
