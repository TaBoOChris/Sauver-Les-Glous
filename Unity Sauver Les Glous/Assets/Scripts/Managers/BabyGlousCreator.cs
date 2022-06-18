using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGlousCreator : MonoBehaviour
{
    [SerializeField] private float m_babyGlousPerc;

    private List<Glou> m_glousAlive = new List<Glou>();

    public List<Glou> CreateChildrenGlous(List<Glou> aliveGlous, int nbGlousAlive)
    {
        List<Glou> babyGlous = new List<Glou>();

        int nbBabyGlous = (int)(nbGlousAlive * m_babyGlousPerc);

        for (int i=0; i<nbBabyGlous; ++i)
        {
            int idParent1 = Random.Range(0, nbGlousAlive);
            int idParent2 = Random.Range(0, nbGlousAlive);

            // Temporaire, le skin du bebe est celui du pere ou de la mere
            Glou.SkinGlou babySkin;
            if (Random.Range(0,2) < 1)
                babySkin = aliveGlous[idParent1].skin;
            else
                 babySkin = aliveGlous[idParent2].skin;

            float babyScale = (aliveGlous[idParent1].sizeMultiplier + aliveGlous[idParent2].sizeMultiplier) / 2;
            Glou babyGlou = new Glou(babySkin, babyScale);

            babyGlous.Add(babyGlou);
        }

        return babyGlous;
    }


}
