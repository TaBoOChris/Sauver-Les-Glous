using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexGUI : MonoBehaviour
{
    [SerializeField] private Canvas _cercleGloumatique;
    private GameObject myEventSystem;

    private void Awake()
    {
        _cercleGloumatique.enabled = false;
        myEventSystem = GameObject.Find("EventSystem");
    }

    public void DisplayCercleGloumatique()
    {
        _cercleGloumatique.enabled = !_cercleGloumatique.enabled;
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
