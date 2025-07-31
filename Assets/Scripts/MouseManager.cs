using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public enum MouseMode { Config, Set, Wire }
    
    public MouseMode mode = MouseMode.Set;

    public enum MouseButtonType { Left, Right }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
    }

    private void HandleRightClick()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            switch (mode)
            {
                case MouseMode.Set:
                    SetActions(MouseButtonType.Right, hit);
                    break;
                case MouseMode.Wire:
                    WireActions(MouseButtonType.Right, hit);
                    break;
                case MouseMode.Config:
                    break;
            }
        }
    }

    private void HandleLeftClick()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            switch (mode)
            {
                case MouseMode.Set:
                    SetActions(MouseButtonType.Left, hit);
                    break;
                case MouseMode.Wire:
                    WireActions(MouseButtonType.Left, hit);
                    break;
                case MouseMode.Config:
                    break;
            }
        }
    }

    private void ConfigActions(MouseButtonType type, RaycastHit2D hit)
    {

    }

    private void SetActions(MouseButtonType type, RaycastHit2D hit)
    {
        switch (type)
        {
            case MouseButtonType.Left:
                var interactableL = hit.collider.GetComponent<MovingComponents>();
                if (interactableL != null)
                {
                    interactableL.OnLeftClick();
                }
                break;
            case MouseButtonType.Right:
                var interactableR = hit.collider.GetComponent<MovingComponents>();
                if (interactableR != null)
                {
                    interactableR.OnRightClick();
                }
                break;
        }
    }

    private void WireActions(MouseButtonType type, RaycastHit2D hit)
    {
        switch (type)
        {
            case MouseButtonType.Left:
                ConnectorPoint point = hit.collider.GetComponent<ConnectorPoint>();
                if (point != null)
                {
                    ConnectionManager.Instance.HandleConnectorClick(point);
                }
                break;
            case MouseButtonType.Right:
                var wire = hit.collider.GetComponent<Wire>();
                if (wire != null)
                {
                    wire.RemoveWire();
                }
                break;
        }
    }
}
