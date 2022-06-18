using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glou
{
    public float sizeMultiplier { get; } //Normal size = 1.0f
    public int? houseID { get; set; }

    public enum SkinGlou {Rouge, Bleu, Jaune, Orange, Vert, Violet,
                       Feu, Dore, Cupcake, Fleur, Glace, Dark}

    private int nbrSkin = 12;

    public SkinGlou skin { get; set; }

    public Glou(SkinGlou skin, float sizeMultiplier)
    {
        this.skin = skin;
        this.sizeMultiplier = sizeMultiplier;
    }

    public void RandomSkin()
    {
        int index = Random.Range(0, nbrSkin);
        this.skin = (SkinGlou)index;
    }
}// var myEnumMemberCount = Enum.GetNames(typeof(MyEnum)).Length;

