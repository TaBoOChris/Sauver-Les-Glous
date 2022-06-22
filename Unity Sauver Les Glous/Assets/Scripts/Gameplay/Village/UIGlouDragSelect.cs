using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIGlouDragSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    /* -------------- Drag And Drop ----------*/
    private bool m_isDragged = false;
    private bool m_isHovered = false;

    private Vector3 m_lastPosition;
    //private int m_sortingOrder;

   
    private void Update()
    {
        // Si le Glou est en train de se faire drag, on le fait suivre le curseur de la souris
        if (m_isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            Vector3 dragPos = mousePos - transform.position;
            transform.position = mousePos;
        }
    }

    // Quand le pointeur est sur le Glou
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Le curseur devient la main
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsPointer())
        {
            cursorManager.SetHand();
        }

        // On indique que le Glou est survolé
        m_isHovered = true;
    }

    // Quand le pointeur sort du Glou
    public void OnPointerExit(PointerEventData eventData)
    {
        // Si le glou est survolé ça veut dire que le joueur est en train de le déplacer
        // Donc on fait rien -> ie : on laisse le curseur en forme de main qui grab
        if (m_isHovered == false)
            return;

        // Sinon, le glou n'est pas en train d'être drag donc le curseur se transforme en flèche
        CursorManager cursorManager = CursorManager.Instance;
        if (cursorManager != null && cursorManager.IsHand())
        {
            cursorManager.SetPointer();
        }
    }

    // Quand on enfonce le bouton de la souris
    public void OnPointerDown(PointerEventData eventData)
    {
        // Le glou est dans l'état "je suis drag" et "la souris me survole"
        m_isDragged = true;
        m_isHovered = true;

        // On enregistre la position du Glou si jamais le joueur le relache dans une zone interdite
        // Il faudra alors TP le Glou à son ancienne position
        m_lastPosition = transform.position;

        //m_sortingOrder = GetComponentInChildren<SpriteRenderer>().sortingOrder;
        //var renderers = GetComponentsInChildren<SpriteRenderer>();

        // Petit "miaaouu" quand le Glou se fait attraper
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGlouCute();
        }

        //for (int i=0; i< renderers.Length; i++)
            //renderers[i].sortingOrder = 15 + i;

        // Le curseur se transforme en main qui grab
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetGrab();

        VillageManager.Instance.ColliderHouses_On();

    }

    // Quand on relache le bouton de la souris
    public void OnPointerUp(PointerEventData eventData)
    {
        // Si le Glou est en train d'être drag
        if (m_isDragged)
        {
            // On indique que ce n'est plus le cas
            m_isDragged = false;

            //var renderers = GetComponentsInChildren<SpriteRenderer>();
            //for (int i = 0; i < renderers.Length; i++)
                //renderers[i].sortingOrder = m_sortingOrder + i;

            // Le curseur se transforme  en main (car le pointeur est toujours sur le Glou au moment de drop)
            if (CursorManager.Instance != null)
                CursorManager.Instance.SetHand();

            // Et on bouge le Glou du sélector au village
            OnMoved();
        }

        VillageManager.Instance.ColliderHouses_Off();
    }

    /* This method is called once a glou is put somewhere */
    private void OnMoved()
    {
        // Si le Glou a bien été drop dans le village
        if (VillageManager.Instance.DropGlouFromUI(this)) // drop is successful
        {
            // Le pointeur se transforme en flèche
            CursorManager.Instance.SetPointer();
            // L'UI du Glou dans le sélector est bien virée
            GameObject.Destroy(this.gameObject);
        }
    }

    // Pour téléporter le Glou à son ancienne position si le joueur le relache dans une zone interdite
    public void JumpToLastPosition()
    {
        transform.position = m_lastPosition;
    }


    public bool IsDragged()
    {
        return m_isDragged;
    }
}
