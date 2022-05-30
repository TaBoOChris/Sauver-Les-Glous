using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    // Temporaire //////////////////////////////////////                   
    struct GlouData
    {
        public float hue;
        public int houseID;
        public float size;

        public GlouData(float hue, int id_m, float size)
        {
            this.hue = hue;
            this.houseID = id_m;
            this.size = size;
        }
    }
    ///////////////////////////////////////////////////

    // Liste récupérée dans le GlouManager
    private List<GlouData> m_glousInVillageData = new List<GlouData>();

    // Liste des Glous dans le village
    private List<GameObject> m_glousInVillage = new List<GameObject>();

    [SerializeField] private GameObject m_glouInVillagePrefab;

    // Le placer soit dans SA maison (60%) soit aléatoirement (40%)
    private void Awake()
    {
        // Temporaire
        m_glousInVillageData.Add(new GlouData(1, 2, 0.18f));
        m_glousInVillageData.Add(new GlouData(0.2f, 2, 0.10f));
        m_glousInVillageData.Add(new GlouData(0.6f, 2, 0.35f));
        m_glousInVillageData.Add(new GlouData(0.8f, 2, 0.24f));
    }

    private void Start()
    {
        SpawnGlousInVillage();
    }


    private void SpawnGlousInVillage()
    {
        foreach (GlouData glouData in m_glousInVillageData)
        {
            // Spawn du glou
            GameObject glou = Instantiate(m_glouInVillagePrefab, new Vector3(Random.Range(-8,8), Random.Range(-4,4), 0), Quaternion.identity, transform);

            // Get glou data
            GlouInVillage data = glou.GetComponent<GlouInVillage>();
            data.SetHue(glouData.hue);
            data.SetHouseID(glouData.houseID);
            data.SetSize(glouData.size);

            // Application des data au glou à faire spawn
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
