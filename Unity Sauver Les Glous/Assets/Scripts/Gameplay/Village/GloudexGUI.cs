using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexGUI : MonoBehaviour
{
    [SerializeField] private GameObject m_cercleGloumatique;
    private GameObject myEventSystem;

    [SerializeField] private SpriteRenderer m_notification;

    private bool isActive = false;

    private void Awake()
    {
        
        myEventSystem = GameObject.Find("EventSystem");
    }

    private void Start()
    {
        m_cercleGloumatique.SetActive(false);

        if (GloudexManager.Instance.GetGlouDecouvertsSize() > 0)
        {
            m_notification.enabled = true;
        }
        else m_notification.enabled = false;
    }

    public void DisplayCercleGloumatique()
    {
        isActive = !isActive;
        m_cercleGloumatique.SetActive(isActive);
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

}
