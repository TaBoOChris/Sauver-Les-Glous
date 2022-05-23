using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDraggable = true;
    public bool alwaysFaceCenter = false;
    private const float divider = 100f; //To adapt on the scale of the level

    private bool isDragged = false;

    private float rotationOffset;

    private PolarCoords2D coords;
    
    // Start is called before the first frame update
    void Start()
    {
        isDragged = false;
        OnMoved();
    }

    private void FixedUpdate()
    {
        if (isDragged) //If being dragged, do not apply rotations
            return;
        float rotationSpeed = LevelProperties.Instance.rotationSpeed;
        if (rotationSpeed == 0.0f) return;

        /* UPDATE POSITION */
        //Increment the angle by a factor of speed - fixed update don't need a deltaTime (since last frame)
        coords.theta += rotationSpeed / divider; 

        coords.HandleAliasing();

        transform.position = coords.ToCartesian();

        /* UPDATE ROTATION */
        FaceCenter();
    }

    private void OnMoved()
    {
        //Calculate offset
        Vector3 relativePos = Vector3.zero - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        rotationOffset = transform.rotation.eulerAngles.z - angle;

        // Debug.Log("rotation offset set to " + rotationOffset);
        coords = new PolarCoords2D(transform.position.x, transform.position.y);
    }

    private void FaceCenter()
    {
        Vector3 relativePos = Vector3.zero - transform.position; //TODO: Point to set instead of Vector3.Zero
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + rotationOffset));
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.yellow;
        OnMoved();
        Gizmos.DrawWireSphere(Vector3.zero, coords.r);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, Vector3.zero);
    }

    /* Dragging */
    //Update only applies to when we drag the platform
    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //Debug.Log(mousePos.x + " , " + mousePos.y);
            mousePos.z = 0;
            Vector3 dragPos = mousePos - transform.position;
            //transform.Translate(dragPos);
            transform.position = mousePos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDraggable) return;
        isDragged = true;
        //Debug.Log("dragging " + name);
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetGrab();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragged)
        {
            isDragged = false;
            OnMoved();
            //Debug.Log("no more dragging " + name);
            if (CursorManager.Instance != null)
                CursorManager.Instance.SetHand();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsPointer())
        {
            cursorManager.SetHand();
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsHand())
        {
            cursorManager.SetPointer();
        }
    }
}
