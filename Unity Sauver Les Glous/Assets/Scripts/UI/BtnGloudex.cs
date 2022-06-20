using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BtnGloudex : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Roue Gloumatique")]
    [SerializeField] private Image _fragmentCouleur;
    [SerializeField] private Sprite _interrogation;
    [SerializeField] private Sprite _exclamation;
    [SerializeField] private Sprite _skinGlou;
    private Image _imageBouton;

    [Header("Infos")]
    [SerializeField] private string _nom;
    [SerializeField] private Glou.SkinGlou skin;
    [SerializeField] private Sprite _formule;

    [SerializeField] private GameObject _infoWindow;
    [SerializeField] private TextMeshProUGUI _nomTMP;
    [SerializeField] private Image _formuleUI;



    private void Awake()
    {
        _imageBouton = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Si le Glou est dans le Gloudex
        if (GloudexManager.Instance.IsInGloudex(skin))
        {
            // Le fragment de couleur est visible
            _fragmentCouleur.color = new Color(1, 1, 1, 1);
            // Le skin du Glou est visible
            _imageBouton.sprite = _skinGlou;
        }
        // Si le Glou vient d'etre découvert
        else if (GloudexManager.Instance.IsWaitingToEnterGloudex(skin))
        {
            // Le fragment de couleur est invisible
            _fragmentCouleur.color = new Color(1, 1, 1, 0);
            // Le skin du Glou est en notification
            _imageBouton.sprite = _exclamation;
        }
        // Le glou n'a pas éte découvert
        else
        {
            // Le fragment de couleur n'est pas visible
            _fragmentCouleur.color = new Color(1, 1, 1, 0);
            // Le skin du Glou est caché
            _imageBouton.sprite = _interrogation;
        }
    }

    public void ClicBouton()
    {
        if(_imageBouton.sprite == _exclamation)
        {
            RevealGlou();
        }
    }

    private void RevealGlou()
    {
        _fragmentCouleur.color = new Color(1, 1, 1, 1);
        _imageBouton.sprite = _skinGlou;
        GloudexManager.Instance.AddGlouInGloudex(skin);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GloudexManager.Instance.IsInGloudex(skin))
        {
            _nomTMP.text = _nom;
            _formuleUI.sprite = _formule;
            _infoWindow.SetActive(true);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _infoWindow.SetActive(false);
    }
}
