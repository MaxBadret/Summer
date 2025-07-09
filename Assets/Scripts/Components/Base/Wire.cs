using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Wire : MonoBehaviour
{
    public ConnectorPoint from;
    public ConnectorPoint to;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    private void Update()
    {
        if (from != null && to != null)
        {
            line.SetPosition(0, from.GetWorldPosition());
            line.SetPosition(1, to.GetWorldPosition());
        }
    }
}