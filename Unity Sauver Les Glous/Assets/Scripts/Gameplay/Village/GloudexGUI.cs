using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexGUI : MonoBehaviour
{
    [SerializeField] private Canvas m_cercleGloumatique;
    private GameObject myEventSystem;

    [SerializeField] private SpriteRenderer m_notification;

    private void Awake()
    {
        
        myEventSystem = GameObject.Find("EventSystem");
    }

    private void Start()
    {
        m_cercleGloumatique.enabled = false;

        if (GloudexManager.Instance.GetGlouDecouvertsSize() > 0)
        {
            m_notification.enabled = true;
        }
        else m_notification.enabled = false;
    }

    public void DisplayCercleGloumatique()
    {
        m_cercleGloumatique.enabled = !m_cercleGloumatique.enabled;
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

}
