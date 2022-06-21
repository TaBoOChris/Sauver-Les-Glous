using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonGlouglou : MonoBehaviour
{
    [SerializeField] private Sprite m_interrogation;
    [SerializeField] private Sprite m_exclamation;
    [SerializeField] private Sprite m_skinGlouglou;
    private Image m_imageBouton;

    [SerializeField] private Image m_notification;
    [SerializeField] private GameObject m_GUInotification;
    private AudioSource _audiosource;

    private GameObject myEventSystem;

    // Start is called before the first frame update
    private void Awake()
    {
        m_imageBouton = GetComponent<Image>();
        myEventSystem = GameObject.Find("EventSystem");
        _audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Si le professeur Glouglou est dans le gloudex
        if(GloudexManager.Instance.getIsProfesseurGlouglouInGloudex())
        {
            m_imageBouton.sprite = m_skinGlouglou;
            m_notification.enabled = false;

        }
        // Sinon si le professeur Glouglou est découvert
        else if(GloudexManager.Instance.getIsProfesseurGlouglouDecouvert() && !GloudexManager.Instance.getIsProfesseurGlouglouInGloudex())
        {
            m_imageBouton.sprite = m_exclamation;
            m_notification.enabled = true;
        }
        // Sinon il manque d'autre glous pour compléter le Gloudex
        else
        {
            m_imageBouton.sprite = m_interrogation;
            m_notification.enabled = false;
        }

    }

    public void ClicBouton()
    {
        if (m_imageBouton.sprite == m_exclamation)
        {
            RevealGlou();
        }

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    private void RevealGlou()
    {
        _audiosource.Play();
        m_imageBouton.sprite = m_skinGlouglou;
        m_notification.enabled = false;
        GloudexManager.Instance.setIsProfesseurGlouglouInGloudex(true);

        m_notification.enabled = false;
        m_GUInotification.SetActive(false);

    }

    public void NotifGlouglou()
    {
        m_notification.enabled = true;
        m_imageBouton.sprite = m_exclamation;
    }
}
