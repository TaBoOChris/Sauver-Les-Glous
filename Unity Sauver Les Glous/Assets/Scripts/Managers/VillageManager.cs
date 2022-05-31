using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageManager : AbstractSingleton<VillageManager>
{
    [SerializeField] private BoxCollider2D m_dropzoneCollider;
    [SerializeField] private Canvas m_canvasBasket;
    [SerializeField] private GameObject m_GlouUIPrefab;

    // Liste des Glous dans le village 
    [Header("Spawn properties")]
    private List<GameObject> m_glousInVillage = new List<GameObject>();
    [SerializeField] private GameObject m_glouInVillagePrefab;

    //Liste des maisons du village (auto enregistr�e)
    [Header("Houses")]
    private List<House> m_villageHouses;
    [SerializeField] GameObject m_housesParent;
    [SerializeField] bool m_SetPositionInHouse = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayVillageMusic();
        }
        m_villageHouses = new List<House>();
        //Add each of the registered houses to the village manager
        foreach (var house in m_housesParent.GetComponentsInChildren<House>())
        {
            m_villageHouses.Add(house);
        }
        Debug.Log("Village registered " + m_villageHouses.Count +" houses.");

        SpawnGlousInVillage();

        foreach (Glou glou in GlousData.Instance.GetGlousInSelector())
        {
            AddGlouInUIBasket(glou);
        }
    }
    private GameObject SpawnGlou(Glou glouData)
    {
        // Spawn du glou
        GameObject glou = Instantiate(m_glouInVillagePrefab, transform.position, Quaternion.identity, transform);

        // Get glou data
        GlouInVillage data = glou.GetComponent<GlouInVillage>();
        data.SetHue(glouData.hue);
        data.SetHouseID(glouData.houseID);
        data.SetSize(glouData.sizeMultiplier);
        data.SetGlou(glouData);

        // Application des data au glou � faire spawn
        SpriteRenderer glouBody = data.GetBodyRenderer();
        SpriteRenderer glouExpression = data.GetExpressionRenderer();

        // couleur
        glouBody.color = Color.HSVToRGB(data.GetHue(), 1, 1);

        // taille
        float size = data.GetSize();
        Vector3 scale = new Vector3(size, size, size);
        glouBody.transform.localScale = scale * 0.2f;
        glouExpression.transform.localScale = scale * 0.2f;

        BoxCollider2D glouCollider = glou.GetComponent<BoxCollider2D>();
        glouCollider.size = new Vector2(size * 2, size * 2);

        return glou;
    }


    private void SpawnGlousInVillage()
    {
        foreach (Glou glouData in GlousData.Instance.GetGlousInVillage())
        {

            GameObject glou = SpawnGlou(glouData);

            // maison -- 60% de chance de spawn dedans
            if (Random.Range(0f,1.0f) >= 0.4f)
            {
                House glouHouse = m_villageHouses.Find(house => house.GetHouseID() == glouData.houseID);
                glou.transform.position = PositionInHouse(glouHouse.GetComponent<BoxCollider2D>().bounds);
                glou.GetComponent<GlouMovement>().enabled = false;
            }
            else
            {
                glou.transform.position = new Vector3(Random.Range(-19.39f, 12.57f), Random.Range(-12.38f, 7.41f), 0);
                glou.GetComponent<GlouMovement>().Awake();
            }

            m_glousInVillage.Add(glou);
        }

    }

    public bool DropGlou(GlouDragSelect glou)
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
                GlousData.Instance.MoveGlouToSelector(ref data);

                //UI Show in basket
                AddGlouInUIBasket(data);
            }
            return true;
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
                        target = PositionInHouse(b);
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
            } // end search
            
            //Fail case : dropped in no interesting position
            if(droppedInAHouse == false)
            {
                glou.JumpToLastPosition();
            }
            else
            {
                glou.GetComponent<GlouMovement>().enabled = false;
            }
            return droppedInAHouse;
        }

    }

    public bool DropGlouFromUI(UIGlouDragSelect glou)
    {
        // Check if within the dropzone
        Vector3 pt = glou.transform.position;

        bool droppedInAHouse = false;

        for (int i = 0; i < m_villageHouses.Count; i++)
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
                    target = PositionInHouse(b);
                }

                var glouPos = b.ClosestPoint(target);
                var villageMono = glou.GetComponent<GlouInVillage>();
                if (villageMono != null)
                {
                    Glou data = villageMono.GetGlou();
                    if (data != null)
                    {
                        data.houseID = m_villageHouses[i].GetHouseID();
                        Debug.Log("Glou assigned to house " + m_villageHouses[i].GetHouseID());

                        GlousData.Instance.MoveGlouToVillage(ref data);

                        GameObject newGlou = SpawnGlou(data);
                        newGlou.transform.position = PositionInHouse(b);
                        newGlou.GetComponent<GlouMovement>().enabled = false;
                    }
                    else Debug.LogWarning("No Glou Reference found in GlouInVillage ! Couldn't assign house");
                }
                else Debug.LogWarning("No GlouInVillage Component found ! Couldn't assign house");

                break;
            }
        }

        //Fail case : dropped in no interesting position
        if (droppedInAHouse == false)
        {
            glou.JumpToLastPosition();
        }
        return droppedInAHouse;
    }

    private void AddGlouInUIBasket(Glou data)
    {
        GameObject glouUI = Instantiate(m_GlouUIPrefab, m_canvasBasket.transform, true);
        glouUI.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        glouUI.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        glouUI.GetComponentInChildren<Image>().color = Color.HSVToRGB(data.hue, 1, 1);
        glouUI.GetComponent<GlouInVillage>().SetGlou(data);
    }

    static Vector3 PositionInHouse(Bounds b)
    {
        return new Vector3(
            Random.Range(b.min.x + 0.5f, b.max.x - 0.5f),
            Random.Range(b.min.y + 0.75f, b.max.y - 0.75f),
            0);
    }


    public void ToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }

}
