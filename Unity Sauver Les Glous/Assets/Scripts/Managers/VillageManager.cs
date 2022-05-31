using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : AbstractSingleton<VillageManager>
{
    [SerializeField] private BoxCollider2D m_dropzoneCollider;
    [SerializeField] private Canvas m_canvasBasket;
    [SerializeField] private GameObject m_GlouUIPrefab;

    // Liste des Glous dans le village 
    [Header("Spawn properties")]
    private List<GameObject> m_glousInVillage = new List<GameObject>();
    [SerializeField] private GameObject m_glouInVillagePrefab;

    //Liste des maisons du village (auto enregistrée)
    [Header("Houses")]
    private List<House> m_villageHouses;
    [SerializeField] GameObject m_housesParent;
    [SerializeField] bool m_SetPositionInHouse = false;

    private void Start()
    {
        m_villageHouses = new List<House>();
        //Add each of the registered houses to the village manager
        foreach (var house in m_housesParent.GetComponentsInChildren<House>())
        {
            m_villageHouses.Add(house);
        }
        Debug.Log("Village registered " + m_villageHouses.Count +" houses.");

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
            GameObject glou = Instantiate(m_glouInVillagePrefab, new Vector3(Random.Range(-19.39f, 12.57f), Random.Range(-12.38f, 7.41f), 0), Quaternion.identity, transform);

            // Get glou data
            GlouInVillage data = glou.GetComponent<GlouInVillage>();
            data.SetHue(glouData.hue);
            data.SetHouseID(glouData.houseID);
            data.SetSize(glouData.sizeMultiplier);
            data.SetGlou(glouData);

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

    public void DropGlou(GlouDragSelect glou)
    {
        // Check if within the dropzone
        Vector3 pt = glou.transform.position;
        if (m_dropzoneCollider.bounds.Contains(pt))
        {
            Debug.Log("Glou dropped inside zone");
            var villageMono = glou.GetComponent<GlouInVillage>();
            if (villageMono != null)
            {
                Glou data = villageMono.GetGlou();
                Destroy(glou.gameObject);

                //Move to selector
                //GlousData.Instance.MoveGlouToSelector(ref data);

                //UI Show in basket
                GameObject glouUI = Instantiate(m_GlouUIPrefab, m_canvasBasket.transform, true);
                glouUI.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                glouUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            }
        } else
        {
            bool droppedInAHouse = false;

            for(int i=0; i< m_villageHouses.Count; i++)
            {
                BoxCollider2D boxc = m_villageHouses[i].GetComponent<BoxCollider2D>();
                if (boxc.bounds.Contains(pt))
                {
                    droppedInAHouse = true;
                    Debug.Log("Glou dropped in house " + m_villageHouses[i].GetHouseID());


                    Bounds b = boxc.bounds;
                    Vector3 target = glou.transform.position;
                    if (m_SetPositionInHouse)
                    {
                        target = new Vector3(
                            Random.Range(b.min.x + 0.2f, b.max.x - 0.2f),
                            Random.Range(b.min.y + 0.2f, b.max.y - 0.2f),
                            0  );
                    }

                    glou.transform.position = b.ClosestPoint(target);
                    var villageMono = glou.GetComponent<GlouInVillage>();
                    if (villageMono != null)
                    {
                        Glou data = villageMono.GetGlou();
                        if (data != null)
                        {
                            data.houseID = m_villageHouses[i].GetHouseID();
                            Debug.Log("Glou assigned to house " + m_villageHouses[i].GetHouseID());
                        } 
                        else Debug.LogWarning("No Glou Reference found in GlouInVillage ! Couldn't assign house");
                    }
                    else Debug.LogWarning("No GlouInVillage Component found ! Couldn't assign house");



                    break;

                }
            } // end searcch
            
            //Fail case : dropped in no interesting position
            if(droppedInAHouse == false)
            {
                glou.JumpToLastPosition();
            }
        }

    }


}
