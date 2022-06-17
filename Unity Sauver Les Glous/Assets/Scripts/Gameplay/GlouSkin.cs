using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouSkin : MonoBehaviour
{
    public enum SkinGlou {Rouge, Bleu, Jaune, Orange, Vert, Violet,
                       Feu, Dore, Cupcake, Fleur, Glace, Dark}

    [SerializeField] private Sprite[] _skinsBase;

    [SerializeField] private SkinGlou _skin = SkinGlou.Rouge;
    [SerializeField] private SpriteRenderer _sr;


    // Start is called before the first frame update
    void Start()
    {
        _sr.sprite = _skinsBase[(int)_skin];
    }

    public void SetSkin(SkinGlou skin)
    {
        _skin = skin;
        _sr.sprite = _skinsBase[(int)_skin];
    }
}
