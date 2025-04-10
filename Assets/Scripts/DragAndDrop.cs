using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    
    Vector3 MousePositionOffset;

    private Vector3 GetMouseWorldPosition()
    {
        //Capture mouse position and return worldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        //Capture mouse offset
        MousePositionOffset =gameObject.transform.position-GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + MousePositionOffset;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

  
}
