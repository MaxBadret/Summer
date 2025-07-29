using UnityEngine;

public class Wire : MonoBehaviour
{
    public ConnectorPoint from;
    public ConnectorPoint to;
    private LineRenderer lineRenderer;
    
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (from != null && to != null && lineRenderer != null)
        {
            Vector3 start = from.GetWorldPosition();
            Vector3 end = to.GetWorldPosition();

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            UpdateCollider(start, end);
        }
    }

    private void UpdateCollider(Vector3 start, Vector3 end)
    {
        if (boxCollider == null) return;

        // Центр между точками
        Vector3 midPoint = (start + end) / 2f;
        transform.position = midPoint;

        // Направление и расстояние
        Vector3 direction = end - start;
        float length = direction.magnitude;

        // Поворот по направлению
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Размеры
        boxCollider.size = new Vector2(length, 0.1f); // 0.1f — толщина
        boxCollider.offset = Vector2.zero;
    }

    // private void OnMouseOver()
    // {
    //     if (Input.GetMouseButtonDown(1)) // правая кнопка мыши
    //     {
    //         RemoveWire();
    //     }
    // }

    public void RemoveWire()
    {
        // Удаляем соединения в обеих точках
        from.ConnectedPoints.Remove(to);
        to.ConnectedPoints.Remove(from);

        // Удаляем из ConnectionManager
        ConnectionManager.Instance.RemoveConnection(from, to);

        // Удаляем визуальный объект
        Destroy(gameObject);
    }
}
