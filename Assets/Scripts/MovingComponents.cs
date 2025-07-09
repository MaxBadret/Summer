using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingComponents : MonoBehaviour
{
    private bool isNeedToMove = false;
    private Vector3 mousePos;
    void Update()
    {
        if (isNeedToMove == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
    
    private void OnMouseDown()
    {
        isNeedToMove = !isNeedToMove;
    }
}
