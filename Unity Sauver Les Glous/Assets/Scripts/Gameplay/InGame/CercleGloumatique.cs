using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercleGloumatique
{
    private List<Glou.SkinGlou> rougeFusionsPossibles = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> bleuFusionsPossibles = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> jauneFusionsPossibles = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> orangeFusionsPossibles = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> vertFusionsPossibles = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> violetFusionsPossibles = new List<Glou.SkinGlou>();

    private List<Glou.SkinGlou> _tousLesSkins = new List<Glou.SkinGlou>();
    private List<Glou.SkinGlou> _babySkins = new List<Glou.SkinGlou>();

    List<Glou.SkinGlou> _selectedList = new List<Glou.SkinGlou>();

    public CercleGloumatique()
    {
        // Tous les skins possibles
        _tousLesSkins.Add(Glou.SkinGlou.Rouge);
        _tousLesSkins.Add(Glou.SkinGlou.Jaune);
        _tousLesSkins.Add(Glou.SkinGlou.Bleu);
        _tousLesSkins.Add(Glou.SkinGlou.Orange);
        _tousLesSkins.Add(Glou.SkinGlou.Vert);
        _tousLesSkins.Add(Glou.SkinGlou.Violet);
        _tousLesSkins.Add(Glou.SkinGlou.Feu);
        _tousLesSkins.Add(Glou.SkinGlou.Dore);
        _tousLesSkins.Add(Glou.SkinGlou.Cupcake);
        _tousLesSkins.Add(Glou.SkinGlou.Fleur);
        _tousLesSkins.Add(Glou.SkinGlou.Glace);
        _tousLesSkins.Add(Glou.SkinGlou.Dark);

        // Toutes les fusions que peut générer le Glou Rouge
        rougeFusionsPossibles.Add(Glou.SkinGlou.Orange);
        rougeFusionsPossibles.Add(Glou.SkinGlou.Violet);
        rougeFusionsPossibles.Add(Glou.SkinGlou.Feu);
        rougeFusionsPossibles.Add(Glou.SkinGlou.Cupcake);

        // Toutes les fusions que peut générer le Glou Jaune
        jauneFusionsPossibles.Add(Glou.SkinGlou.Vert);
        jauneFusionsPossibles.Add(Glou.SkinGlou.Orange);
        jauneFusionsPossibles.Add(Glou.SkinGlou.Dore);
        jauneFusionsPossibles.Add(Glou.SkinGlou.Fleur);

        // Toutes les fusions que peut générer le Glou Bleu
        bleuFusionsPossibles.Add(Glou.SkinGlou.Vert);
        bleuFusionsPossibles.Add(Glou.SkinGlou.Violet);
        bleuFusionsPossibles.Add(Glou.SkinGlou.Glace);
        bleuFusionsPossibles.Add(Glou.SkinGlou.Dark);

        // Toutes les fusions que peut générer le Glou Orange
        orangeFusionsPossibles.Add(Glou.SkinGlou.Feu);
        orangeFusionsPossibles.Add(Glou.SkinGlou.Dore);

        // Toutes les fusions que peut générer le Glou Vert
        vertFusionsPossibles.Add(Glou.SkinGlou.Fleur);
        vertFusionsPossibles.Add(Glou.SkinGlou.Glace);

        // Toutes les fusions que peut générer le Glou Violet
        violetFusionsPossibles.Add(Glou.SkinGlou.Cupcake);
        violetFusionsPossibles.Add(Glou.SkinGlou.Dark);
    }

    public Glou Fusion(Glou glou1, Glou glou2)
    {
        Debug.Log("Fusion d'un glou " + glou1.skin + " et d'un glou " + glou2.skin);
        List<Glou.SkinGlou> listeDesFusions = new List<Glou.SkinGlou>();

        // On récupère les deux listes contenant les skins possibles pour le nouveau glou
        List<Glou.SkinGlou> skinPossibilitiesParent1 = new List<Glou.SkinGlou>(SelectListFusionPossibles(glou1));
        List<Glou.SkinGlou> skinPossibilitiesParent2 = new List<Glou.SkinGlou>(SelectListFusionPossibles(glou2));

        // On récupère les skins commun au deux listes
        foreach(Glou.SkinGlou skinP1 in skinPossibilitiesParent1)
        {
            foreach (Glou.SkinGlou skinP2 in skinPossibilitiesParent2)
            {
                if(skinP1 == skinP2)
                {
                    Debug.Log("Skin en commun : " + skinP1 + " et " + skinP2);
                    listeDesFusions.Add(skinP1);
                }
                else
                {
                    Debug.Log(skinP1 + " et " + skinP2 + " ne sont pas des skins en commun");
                }
            }
        }

        Debug.Log("Resultats :");

        Debug.Log("Nombre de skins restants : " + listeDesFusions.Count);

        // Si il n'y a qu'un seul skin possible alors la fusion a réussie, on retourne ce Glou
        if(listeDesFusions.Count == 1)
        {
            Glou.SkinGlou babySkin = listeDesFusions[0];
            float babyScale = (glou1.sizeMultiplier +glou2.sizeMultiplier) / 2;
            return new Glou(babySkin, babyScale);
        }
        // Sinon il y a 0 skins possibles ou plusieurs skins possibles, la fusion a échouée
        else
        {
            Debug.Log("LA FUSION A ECHOUÉE");
            return glou1;
        }
    }

    private List<Glou.SkinGlou> SelectListFusionPossibles(Glou glou)
    {
        if (glou.skin == Glou.SkinGlou.Rouge)
            return rougeFusionsPossibles;
        else if (glou.skin == Glou.SkinGlou.Jaune)
            return jauneFusionsPossibles;
        else if (glou.skin == Glou.SkinGlou.Bleu)
            return bleuFusionsPossibles;
        else if (glou.skin == Glou.SkinGlou.Vert)
            return vertFusionsPossibles;
        else if (glou.skin == Glou.SkinGlou.Orange)
            return orangeFusionsPossibles;
        else if (glou.skin == Glou.SkinGlou.Violet)
            return violetFusionsPossibles;
        else
            return new List<Glou.SkinGlou>();
    }

    
}
