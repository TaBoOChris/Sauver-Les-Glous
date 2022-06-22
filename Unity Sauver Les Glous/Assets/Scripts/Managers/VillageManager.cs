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
    private int m_maxGlousPrimaires = 4;

    //Liste des maisons du village (auto enregistr�e)
    [Header("Houses")]
    private List<House> m_villageHouses;
    [SerializeField] GameObject m_housesParent;
    [SerializeField] bool m_SetPositionInHouse = false;

    private void Start()
    {
        Time.timeScale = 1.0f;

        // Lecture de la musique du village
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayVillageMusic();
        }

        // On ajoute toutes les maisons dans une liste de maison
        m_villageHouses = new List<House>();
        foreach (var house in m_housesParent.GetComponentsInChildren<House>())
            m_villageHouses.Add(house);
        //Debug.Log("Village registered " + m_villageHouses.Count +" houses.");

        // On fait spawn tous les Glous dans le village
        // Ceux qui étaient déjà dans le village
        // Mais aussi ceux qui viennent du sélector
        SpawnGlousInVillage();

        // Tous les Glous qui sont dans le sélector sont insant déplacés du village (visuellement) au panier
        foreach (Glou glou in GlousData.Instance.GetGlousInSelector())
            AddGlouInUIBasket(glou);

        // Pour le Gloudex ///////////////////////////////////////////////////////////////////////////////////////////
        GloudexManager.Instance.DetectNewGlou();
        // Pour le Gloudex ///////////////////////////////////////////////////////////////////////////////////////////
    }

    // Récupère tous les Glous qui étaient restés dans le village
    // Récupère tous les Glous qui sont dans le panier
    // Récupère tous les Glous à regénérer
    // Puis place tout le monde dans le village
    private void SpawnGlousInVillage()
    {
        // Régénération des Glous Rouge, Jaune et Bleu
        // Ajout de ces derniers à la liste des Glous du village dans GlousData
        RegenerateRYBGlous();

        // On fait spawn dans le village tous les Glous qui sont dans la liste des Glous_au_village dans GlousData
        foreach (Glou glouData in GlousData.Instance.GetGlousInVillage())
        {
            // Création du Glou
            GameObject glou = SpawnGlou(glouData);

            // 40% de chance de spawn dans une maison (Glou fixe)
            if (Random.Range(0f, 1.0f) >= 0.6f)
            {
                House glouHouse = m_villageHouses.Find(house => house.GetHouseID() == glouData.houseID);
                glou.transform.position = PositionInHouse(glouHouse.GetComponent<BoxCollider2D>().bounds);
                glou.GetComponent<GlouMovement>().enabled = false;
            }
            // 60% de chance de spawn à l'extérieur (Glou qui se balade)
            else
            {
                glou.transform.position = new Vector3(Random.Range(-19.39f, 12.57f), Random.Range(-12.38f, 7.41f), 0);
                glou.GetComponent<GlouMovement>().Awake();
            }

            ////////////////////////////////////////////////////////////////////////////////////
            // Est ce que cette liste a un intêret sachant qu'il y a la même dans Glousdata ?
            m_glousInVillage.Add(glou);
            ///////////////////////////////////////////////////////////////////////////////////
        }

    }

    // Créé et retourne un Glou à faire spawn dans le village à partir des datas de ce Glous
    private GameObject SpawnGlou(Glou glouData)
    {
        // Création d'un nouveau Glou
        GameObject glou = Instantiate(m_glouInVillagePrefab, transform.position, Quaternion.identity, transform);

        // Récupération de tous ses attributs
        GlouInVillage data = glou.GetComponent<GlouInVillage>();
        data.SetSkin(glouData.skin);
        data.SetHouseID(glouData.houseID);
        data.SetSize(glouData.sizeMultiplier);
        data.SetGlou(glouData);

        // Application des attributs au nouveau Glou
        
        // skin (il s'appliquera automatiquement grace au script skinGlou)
        SpriteRenderer glouBody = data.GetBodyRenderer();
        SpriteRenderer glouExpression = data.GetExpressionRenderer();
        
        // taille
        float size = data.GetSize();
        Vector3 scale = new Vector3(size, size, size);
        glouBody.transform.localScale = scale * 0.2f;
        glouExpression.transform.localScale = scale * 0.2f;

        BoxCollider2D glouCollider = glou.GetComponent<BoxCollider2D>();
        glouCollider.size = new Vector2(size * 2, size * 2);

        return glou;
    }

    // Compte le nombre de Glous Rouge, Jaune et Bleu actuellement dans le village et le sélector
    // Puis appelle RegenerateGlou(nbr, Glou.SkinGlou) pour ajouter 4 - nbr Glous (pour chaque skin RYB)
    private void RegenerateRYBGlous()
    {
        // On compte le nombre de Glous déjà dans le sélector et le village
        // Pour ajouter le bon nombre de Glous pour que ça fasse bien 4 de chaque à la fin
        int nbrRouge = 0;
        int nbrJaune = 0;
        int nbrBleu = 0;

        // Nombre de Glous RYB dans le selector
        foreach (Glou gloudata in GlousData.Instance.GetGlousInSelector())
        {
            if (gloudata.skin == Glou.SkinGlou.Rouge) nbrRouge++;
            if (gloudata.skin == Glou.SkinGlou.Jaune) nbrJaune++;
            if (gloudata.skin == Glou.SkinGlou.Bleu) nbrBleu++;
        }

        // Nombre de Glous RYB dans le village
        foreach (Glou gloudata in GlousData.Instance.GetGlousInVillage())
        {
            if (gloudata.skin == Glou.SkinGlou.Rouge) nbrRouge++;
            if (gloudata.skin == Glou.SkinGlou.Jaune) nbrJaune++;
            if (gloudata.skin == Glou.SkinGlou.Bleu) nbrBleu++;
        }

        RegenerateGlou(nbrRouge, Glou.SkinGlou.Rouge);
        RegenerateGlou(nbrJaune, Glou.SkinGlou.Jaune);
        RegenerateGlou(nbrBleu, Glou.SkinGlou.Bleu);
    }

    // Ajoute au village 4 - nbrGlous du skin renseigné
    // (Les Glous ne sont pas directement placés dans le village, mais son ajoutés dans la liste
    // des Glous du village dans GlousData)
    private void RegenerateGlou(int nbrGlous, Glou.SkinGlou skin)
    {
        // Limitation du nombre de Glous
        if (nbrGlous > m_maxGlousPrimaires) nbrGlous = m_maxGlousPrimaires;

        // Ajout des Glous de couleur primaire dans le village
        for (int i = 0; i < m_maxGlousPrimaires - nbrGlous; i++)
        {
            GlousData.Instance.AddGlouToVillage(new Glou(skin, Random.Range(0.6f, 1.2f)), Random.Range(0, m_villageHouses.Count));
        }
    }

    // Le glou doit venir du village (ie : pas du sélector)
    // Gère quand le joueur drop un Glou dans une maison, à l'extérieur ou dans un endroit interdit
    public bool DropGlou(GlouDragSelect glou)
    {
        // On récupère la position du Glou dans le village (ie : la position où le joueur à drag le glou)
        Vector3 pt = glou.transform.position;

        // Si cette position est dans la dropzone du panier et qu'il n'y a pas déjà 10 Glous dedans
        if (m_dropzoneCollider.bounds.Contains(pt) && GlousData.Instance.GetGlousInSelectorCount() < 10)
        {
            Debug.Log("M_DropGlouGlou : dropped inside panier");

            // Alors il faut transformer le GlousInVillage en GlousUI (ie : un Glou dans le panier)
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
        } 
        
        // Sinon, on regarde si le Glou a été drop dans une maison
        else
        {
            bool droppedInAHouse = false;

            // Pour chaque maison
            for(int i=0; i< m_villageHouses.Count; i++)
            {
                // On regarde si le Glou est dans la dropzone de cette maison
                BoxCollider2D boxc = m_villageHouses[i].GetComponent<BoxCollider2D>();
                if (boxc.bounds.Contains(pt))
                {
                    droppedInAHouse = true;
                    Debug.Log("M_DropGlouGlou : Glou dropped in house " + m_villageHouses[i].GetHouseID());


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
                            Debug.Log("M_DropGlouGlou : Glou assigned to house " + m_villageHouses[i].GetHouseID());
                        } 
                        else Debug.LogWarning("M_DropGlouGlou : No Glou Reference found in GlouInVillage ! Couldn't assign house");
                    }
                    else Debug.LogWarning("M_DropGlouGlou : No GlouInVillage Component found ! Couldn't assign house");

                    break;

                }
            } // end search
            
            //Fail case : dropped in no interesting position
            if(droppedInAHouse == false)
            {
                Debug.LogWarning("M_DropGlouGlou : Glou dropped in no interesting position");
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
        glouUI.GetComponent<GlouSkinUI>().SetSkin(data.skin);
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
        AudioManager.Instance.StopMusic();
        if (GlousData.Instance.GetGlousInSelector().Count > 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayLvl");
    }

}
