using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlouSkinUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _skinsBase;

    [SerializeField] private Image _image;

    public Glou.SkinGlou _skin;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponentInChildren<Image>();

        _image.sprite = _skinsBase[(int)_skin];
    }

    public void SetSkin(Glou.SkinGlou skin)
    {
        _skin = skin;
        _image.sprite = _skinsBase[(int)_skin];
    }

}
