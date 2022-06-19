using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGlousCreator : MonoBehaviour
{
    private List<Glou> m_glousAlive = new List<Glou>();

    public List<Glou> CreateChildrenGlous(List<Glou> aliveGlous, int nbGlousAlive)
    {
        List<Glou> babyGlous = new List<Glou>();

        // Cr�ation de 1 b�be par 2 glous
        int nbBabyGlous = (int)(nbGlousAlive/2);

        List<Glou> parents = new List<Glou>(aliveGlous);

        for (int i = 0; i < nbBabyGlous; ++i)
        {
            // S�lection du premier parent
            int id = Random.Range(0, parents.Count);
            Glou parent1 = parents[id];
            parents.RemoveAt(id);

            // S�lection du second parent
            id = Random.Range(0, parents.Count);
            Glou parent2 = parents[id];
            parents.RemoveAt(id);

            CercleGloumatique fusionneur = new CercleGloumatique();
            Glou babyGlou = fusionneur.Fusion(parent1, parent2);

            // Si la fusion a r�ussie alors on retourne le b�b�
            if (babyGlou != parent1)
            {
                Debug.Log("Nouveau Glou cr��");
                babyGlous.Add(babyGlou);
            }
            else Debug.Log("Le b�b� est identique au parent");

        }

        return babyGlous;
    }


}
