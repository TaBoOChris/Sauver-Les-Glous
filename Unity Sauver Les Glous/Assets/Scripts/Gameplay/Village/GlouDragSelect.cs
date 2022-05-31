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

    private Vector3 m_lastPosition;
    private int m_sortingOrder;

    private void Start()
    {
        
    }

    /* This method is called once a glou is put somewhere */
    private void OnMoved()
    {
        if (VillageManager.Instance.DropGlou(this)) // drop is successful
            CursorManager.Instance.SetPointer();
    }

    public void JumpToLastPosition()
    {
        transform.position = m_lastPosition;
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
        m_lastPosition = transform.position;
        m_sortingOrder = GetComponentInChildren<SpriteRenderer>().sortingOrder;
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i=0; i< renderers.Length; i++)
        {
            renderers[i].sortingOrder = 15 + i;
        }
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetGrab();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_isDragged)
        {
            m_isDragged = false;
            var renderers = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sortingOrder = m_sortingOrder + i;
            }
            if (CursorManager.Instance != null)
                CursorManager.Instance.SetHand();
            OnMoved();
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

    public bool IsDragged()
    {
        return m_isDragged;
    }
}
