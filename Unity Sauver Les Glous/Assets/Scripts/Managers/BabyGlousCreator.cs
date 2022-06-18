using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGlousCreator : MonoBehaviour
{
    private List<Glou> m_glousAlive = new List<Glou>();

    public List<Glou> CreateChildrenGlous(List<Glou> aliveGlous, int nbGlousAlive)
    {
        List<Glou> babyGlous = new List<Glou>();

        // Création de 1 bébe par 2 glous
        int nbBabyGlous = (int)(nbGlousAlive/2);

        List<Glou> parents = new List<Glou>(aliveGlous);

        for (int i = 0; i < nbBabyGlous; ++i)
        {
            // Sélection du premier parent
            int id = Random.Range(0, parents.Count);
            Glou parent1 = parents[id];
            parents.RemoveAt(id);

            // Sélection du second parent
            id = Random.Range(0, parents.Count);
            Glou parent2 = parents[id];
            parents.RemoveAt(id);

            CercleGloumatique fusionneur = new CercleGloumatique();
            Glou babyGlou = fusionneur.Fusion(parent1, parent2);

            // Si la fusion a réussie alors on retourne le bébé
            if (babyGlou != parent1)
            {
                Debug.Log("Nouveau Glou créé");
                babyGlous.Add(babyGlou);
            }
            else Debug.Log("Le bébé est identique au parent");

        }

        return babyGlous;
    }


}
