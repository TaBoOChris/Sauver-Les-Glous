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

            float babyHue = (aliveGlous[idParent1].hue + aliveGlous[idParent2].hue) / 2;
            float babyScale = (aliveGlous[idParent1].sizeMultiplier + aliveGlous[idParent2].sizeMultiplier) / 2;
            Glou babyGlou = new Glou(babyHue, babyScale);

            babyGlous.Add(babyGlou);
        }

        return babyGlous;
    }


}
