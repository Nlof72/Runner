using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main; 
    }

    public bool CheckPositionCursor() => Input.mousePosition.y < (Screen.height / 2 + Screen.height / 8);

    public Vector3 GetDirectionToClick(Vector3 playerPosition)
    {
        Vector3 mousePosiion = Input.mousePosition;

        mousePosiion = _camera.ScreenToViewportPoint(mousePosiion);
        mousePosiion.z = 2;
        mousePosiion = _camera.ViewportToWorldPoint(mousePosiion);
        Vector3 direction = new Vector3(mousePosiion.x - playerPosition.x, 0, 0);

        return direction;
    }
}
