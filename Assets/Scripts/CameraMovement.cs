using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour

{
    private bool drag = false;
    private Vector3 previous;

    public bool IsDragging() {
        return drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (!drag && !Input.GetMouseButton(0)) return;
        if (drag && !Input.GetMouseButton(0)) {
            drag = false;
        }
        if (!drag && Input.GetMouseButton(0)) {
            drag = true;
            previous = Input.mousePosition;
        }
        if (drag && Input.GetMouseButton(0)) {
            Vector3 destination = Input.mousePosition;
            float angle = destination.x - previous.x;
            transform.RotateAround(new Vector3(-12, -0.05f, -14.1f), Vector3.up, angle/10);
            previous = destination;
        }
    }
}
