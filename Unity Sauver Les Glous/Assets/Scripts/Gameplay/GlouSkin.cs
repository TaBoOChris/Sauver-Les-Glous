using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouSkin : MonoBehaviour
{
    [SerializeField] private Sprite[] _skinsBase;

    [SerializeField] private SpriteRenderer _sr;

    public Glou.SkinGlou _skin;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<GlouInGame>())
        {
            _skin = GetComponent<GlouInGame>().GetGlou().skin;
        }
        else if (GetComponent<GlouInVillage>())
        {
            _skin = GetComponent<GlouInVillage>().GetGlou().skin;
        }

        _sr.sprite = _skinsBase[(int)_skin];
    }

    public void SetSkin(Glou.SkinGlou skin)
    {
        _skin = skin;
        _sr.sprite = _skinsBase[(int)_skin];
    }
}
