using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glou
{
    public float sizeMultiplier { get; } //Normal size = 1.0f
    public int? houseID { get; set; }

    public enum SkinGlou {Rouge, Bleu, Jaune, Orange, Vert, Violet,
                       Feu, Dore, Cupcake, Fleur, Glace, Dark}

    public SkinGlou skin { get; set; }

    public Glou(SkinGlou skin, float sizeMultiplier)
    {
        this.skin = skin;
        this.sizeMultiplier = sizeMultiplier;
    }

    // Retourne le skin Rouge, Jaune, Bleu, Violet, Vert ou Orange
    public static SkinGlou RandomSkinRYBOGP()
    {
        return (SkinGlou)Random.Range(0, 6);
    }

    // Retourne le skin Rouge, Jaune ou Bleu
    public static SkinGlou RandomSkinRYB()
    {
        return (SkinGlou)Random.Range(0, 3);
    }
}

