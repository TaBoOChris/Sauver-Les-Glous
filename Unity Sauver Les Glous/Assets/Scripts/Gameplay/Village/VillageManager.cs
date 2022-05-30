using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    // Liste des Glous dans le village 
    private List<GameObject> m_glousInVillage = new List<GameObject>();

    [SerializeField] private GameObject m_glouInVillagePrefab;

   
    private void Start()
    {
        // temporaire
        GlousData.Instance.AddGlouToVillage(new Glou(0.1f, 0.14f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.2f, 0.18f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.3f, 0.24f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.4f, 0.28f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.5f, 0.31f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.6f, 0.12f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.7f, 0.11f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.8f, 0.18f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.9f, 0.18f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(1, 0.18f), 0);

        SpawnGlousInVillage();
    }

    private void SpawnGlousInVillage()
    {
        foreach (Glou glouData in GlousData.Instance.GetGlousInVillage())
        {
            // Spawn du glou
            GameObject glou = Instantiate(m_glouInVillagePrefab, new Vector3(Random.Range(-8,8), Random.Range(-4,4), 0), Quaternion.identity, transform);

            // Get glou data
            GlouInVillage data = glou.GetComponent<GlouInVillage>();
            data.SetHue(glouData.hue);
            data.SetHouseID(glouData.houseID);
            data.SetSize(glouData.sizeMultiplier);

            // Application des data au glou � faire spawn
            SpriteRenderer glouBody = data.GetBodyRenderer();
            SpriteRenderer glouExpression = data.GetExpressionRenderer();

            // couleur
            glouBody.color = Color.HSVToRGB(data.GetHue(), 1, 1);

            // taille
            float size = data.GetSize();
            Vector3 scale = new Vector3(size, size, size);
            glouBody.transform.localScale = scale;
            glouExpression.transform.localScale = scale;

            // maison
            // to do


            m_glousInVillage.Add(glou);
        }          
        
    }
    

} // class
