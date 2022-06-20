using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexGUI : MonoBehaviour
{
    [SerializeField] private Canvas m_cercleGloumatique;
    private GameObject myEventSystem;

    private void Awake()
    {
        m_cercleGloumatique.enabled = false;
        myEventSystem = GameObject.Find("EventSystem");
    }

    public void DisplayCercleGloumatique()
    {
        m_cercleGloumatique.enabled = !m_cercleGloumatique.enabled;
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
