using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlatformStock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDraggable = true;
    private bool isHovered = false;
    private bool isDragged = false;

    private GameObject DeadZoneCircle;

    // Start is called before the first frame update
    void Start()
    {
        isDragged = false;
        DeadZoneCircle = GameObject.FindWithTag("DrumLimit");
    }

    /* Dragging */
    //Update only applies to when we drag the platform
    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            Vector3 dragPos = mousePos - transform.position;
            transform.position = mousePos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDraggable || GameManager.Instance.IsGamePaused())
            return; //Abort if not draggable or if the game is paused
        isDragged = true;
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        isHovered = true;
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetGrab();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragged)
        {
            isDragged = false;
            gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            if (CursorManager.Instance != null)
                CursorManager.Instance.SetHand();

            float scaledRadius = Mathf.Max(DeadZoneCircle.transform.localScale.x, DeadZoneCircle.transform.localScale.y);
            float scaledCircleCollider = DeadZoneCircle.GetComponent<CircleCollider2D>().radius;
            float dist = Vector3.Distance(Vector3.zero, transform.position);
            if (dist < scaledCircleCollider * scaledRadius * 1.05f)
            {
                gameObject.GetComponent<Platform>().enabled = true;
                gameObject.GetComponent<PlatformStock>().enabled = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDraggable || GameManager.Instance.IsGamePaused())
            return; //Abort if not draggable or if the game is paused
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsPointer())
        {
            cursorManager.SetHand();
        }
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered == false)
            return; //Abort if the platform was not hovered in the first place.
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsHand())
        {
            cursorManager.SetPointer();
        }
    }
}
