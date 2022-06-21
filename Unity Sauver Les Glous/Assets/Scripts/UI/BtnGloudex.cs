using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BtnGloudex : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Roue Gloumatique")]
    [SerializeField] private Image m_fragmentCouleur;
    [SerializeField] private Sprite m_interrogation;
    [SerializeField] private Sprite m_exclamation;
    [SerializeField] private Sprite m_skinGlou;
    private Image m_imageBouton;

    [Header("Infos")]
    [SerializeField] private string m_nom;
    [SerializeField] private Glou.SkinGlou m_skin;
    [SerializeField] private Sprite m_formule;

    [SerializeField] private GameObject m_infoWindow;
    [SerializeField] private TextMeshProUGUI m_nomTMP;
    [SerializeField] private Image m_formuleUI;

    [SerializeField] private Image m_notification;
    [SerializeField] private GameObject m_GUInotification;
    [SerializeField] private Image m_PGlouglounotification;
    private AudioSource _audiosource;

    [SerializeField] private BoutonGlouglou boutonGlouglou;

    private GameObject myEventSystem;

    private void Awake()
    {
        m_imageBouton = GetComponent<Image>();
        myEventSystem = GameObject.Find("EventSystem");
        _audiosource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Si le Glou est dans le Gloudex
        if (GloudexManager.Instance.IsInGloudex(m_skin))
        {
            //Debug.Log("Le " + m_skin + " est dans le Gloudex");
            // Le fragment de couleur est visible
            m_fragmentCouleur.color = new Color(1, 1, 1, 1);
            // Le skin du Glou est visible
            m_imageBouton.sprite = m_skinGlou;
            m_notification.enabled = false;
        }
        // Si le Glou vient d'etre découvert
        else if (GloudexManager.Instance.IsWaitingToEnterGloudex(m_skin))
        {
            //Debug.Log("Le " + m_skin + " vient d'etre découvert");
            // Le fragment de couleur est invisible
            m_fragmentCouleur.color = new Color(1, 1, 1, 0);
            // Le skin du Glou est en notification
            m_imageBouton.sprite = m_exclamation;
            m_notification.enabled = true;
        }
        // Le glou n'a pas éte découvert
        else
        {
            //Debug.Log("Le " + m_skin + " n'est pas découvert");
            // Le fragment de couleur n'est pas visible
            m_fragmentCouleur.color = new Color(1, 1, 1, 0);
            // Le skin du Glou est caché
            m_imageBouton.sprite = m_interrogation;
            m_notification.enabled = false;
        }
    }

    public void ClicBouton()
    {
        if(m_imageBouton.sprite == m_exclamation)
        {
            RevealGlou();

            // Si tous les Glous sont dans le Gloudex, on découvre le professeur Glouglou
            if (GloudexManager.Instance.getIsProfesseurGlouglouDecouvert() && !GloudexManager.Instance.getIsProfesseurGlouglouInGloudex())
            {
                // Notif du Gui
                m_GUInotification.SetActive(true);

                // Notif du glouglou
                boutonGlouglou.NotifGlouglou();

            }
        }

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    private void RevealGlou()
    {
        _audiosource.Play();
        m_fragmentCouleur.color = new Color(1, 1, 1, 1);
        m_imageBouton.sprite = m_skinGlou;
        m_notification.enabled = false;
        GloudexManager.Instance.AddGlouInGloudex(m_skin);

        if (GloudexManager.Instance.GetGlouDecouvertsSize() == 0)
            m_GUInotification.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GloudexManager.Instance.IsInGloudex(m_skin))
        {
            m_nomTMP.text = m_nom;
            m_formuleUI.sprite = m_formule;
            m_infoWindow.SetActive(true);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_infoWindow.SetActive(false);
    }
}
