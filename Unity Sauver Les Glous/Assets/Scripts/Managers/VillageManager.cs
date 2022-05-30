using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : AbstractSingleton<VillageManager>
{
    [SerializeField] private BoxCollider2D m_dropzoneCollider;
    [SerializeField] private Canvas m_canvasBasket;
    [SerializeField] private GameObject m_GlouUIPrefab;

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
            Debug.Log("Glou dropped somewhere");
        }

    }


}
