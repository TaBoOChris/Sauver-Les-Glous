using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlouDragSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    /* -------------- Drag And Drop ----------*/
    private bool m_isDragged = false;
    private bool m_isHovered = false;

    /* This method is called once a glou is put somewhere */
    private void OnMoved()
    {
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetPointer();
        VillageManager.Instance.DropGlou(this);
    }

    private void Update()
    {
        if (m_isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            Vector3 dragPos = mousePos - transform.position;
            transform.position = mousePos;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        m_isDragged = true;
        m_isHovered = true;
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetGrab();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_isDragged)
        {
            m_isDragged = false;
            OnMoved();
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
        m_isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_isHovered == false)
            return; //Abort if the platform was not hovered in the first place.
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsHand())
        {
            cursorManager.SetPointer();
        }
    }
}
