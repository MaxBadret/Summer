using UnityEngine;

public class MovingComponents : MonoBehaviour
{
    private bool isNeedToMove = false;
    private Vector3 mousePos;

    private RectTransform movementArea;

    private Vector3 minWorldPos;
    private Vector3 maxWorldPos;

    private bool boundsInitialized = false;

    [SerializeField] GameObject circle1;

    [SerializeField] GameObject circle2;

    void Start()
    {
        //Поиск RectTransform с названием "MoveArea"
        GameObject areaObject = GameObject.Find("Level");
        if (areaObject != null)
        {
            movementArea = areaObject.GetComponent<RectTransform>();
            if (movementArea != null)
                CalculateBounds();
            else
                Debug.LogError("MoveArea найден, но не содержит RectTransform!");
        }
        else
        {
            Debug.LogError("MoveArea не найден на сцене!");
        }
    }

    void Update()
    {
        if (isNeedToMove && movementArea != null && boundsInitialized)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Ограничение в пределах прямоугольной области
            mousePos.x = Mathf.Clamp(mousePos.x, minWorldPos.x, maxWorldPos.x);
            mousePos.y = Mathf.Clamp(mousePos.y, minWorldPos.y, maxWorldPos.y);

            transform.position = mousePos;
        }

        if (MouseManager.Instance.mode == MouseManager.MouseMode.Wire)
        {
            CircleSetActiv(true);
        }
        else
        {
            CircleSetActiv(false);
        }
    }

    public void OnLeftClick()
    {
        isNeedToMove = !isNeedToMove;
    }

    public void OnRightClick()
    {
        ConnectionManager.Instance.RemoveAllConnectionsWith(GetComponent<BaseComponent>());
        Destroy(gameObject);
    }

    private void CalculateBounds()
    {
        Vector3[] corners = new Vector3[4];
        movementArea.GetWorldCorners(corners); // 0 - нижний левый, 2 - верхний правый

        minWorldPos = corners[0];
        maxWorldPos = corners[2];

        boundsInitialized = true;
    }

    public void CircleSetActiv(bool isActriv)
    {
        circle1.SetActive(isActriv);
        circle2.SetActive(isActriv);
    }
}
