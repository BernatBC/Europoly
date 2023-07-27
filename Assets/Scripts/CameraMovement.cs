using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Class <c>CameraMovement</c> contains the mothods of the camera movements.
/// </summary>
public class CameraMovement : MonoBehaviour

{
    /// <summary>
    /// bool <c>movementAllowed</c> indicates if on mouse drag, the board moves
    /// </summary>
    private bool movementAllowed;

    /// <summary>
    /// bool <c>drag</c> indicates if the mouse is in dragging mode.
    /// </summary>
    private bool drag = false;

    /// <summary>
    /// Vector3 <c>previous</c> camera position in the last frame.
    /// </summary>
    private Vector3 previous;

    /// <summary>
    /// Method <c>EnableMovement</c> modifies <c>movementAllowed</c> boolean.
    /// </summary>
    /// <param name="enable">true if movement is allowed, false otherwise.</param>
    public void EnableMovement(bool enable) {
        movementAllowed = enable;
    }

    /// <summary>
    /// Method <c>Update</c> calculates the new camera position when dragging the mouse.
    /// </summary>
    void Update()
    {
        if (!movementAllowed) return;

        if (drag)
        {
            if (!Input.GetMouseButton(0)) {
                drag = false;
                return;
            }
            Vector3 destination = Input.mousePosition;
            float angle = destination.x - previous.x;
            transform.RotateAround(new Vector3(-12, -0.05f, -14.1f), Vector3.up, angle / 10);
            previous = destination;
        }
        else if (Input.GetMouseButton(0))
        {
            drag = true;
            previous = Input.mousePosition;
        }
    }
}
