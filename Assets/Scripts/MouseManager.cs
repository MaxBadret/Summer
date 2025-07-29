using UnityEngine;

public class MouseManager : MonoBehaviour
{
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
            var interactable = hit.collider.GetComponent<MovingComponents>();
            if (interactable != null)
            {
                interactable.OnRightClick();
            }

            var wire = hit.collider.GetComponent<Wire>();
            if (wire != null)
            {
                wire.RemoveWire();
            }
        }
    }
    
    private void HandleLeftClick()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            var interactable = hit.collider.GetComponent<MovingComponents>();
            if (interactable != null)
            {
                interactable.OnLeftClick();
            }
        }
    }
}
