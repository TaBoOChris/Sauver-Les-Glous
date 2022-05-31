using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreinitList : MonoBehaviour
{
    [SerializeField] List<Glou> m_glousStartingList = new List<Glou> { new Glou(0.2f, 0.8f), new Glou(0.5f, 1f), new Glou(0.8f, 1.2f) };
    // Start is called before the first frame update
    void Start()
    {
        GlousData.Instance.SetGlousInSelector(m_glousStartingList);
        Debug.Log("Glous in selector : " + GlousData.Instance.GetGlousInSelector().Count);

        GlousData.Instance.AddGlouToVillage(new Glou(0.1f, 0.14f), 0);
        GlousData.Instance.AddGlouToVillage(new Glou(0.2f, 0.18f), 1);
        GlousData.Instance.AddGlouToVillage(new Glou(0.3f, 0.24f), 1);
        GlousData.Instance.AddGlouToVillage(new Glou(0.4f, 0.28f), 2);
        GlousData.Instance.AddGlouToVillage(new Glou(0.5f, 0.31f), 3);
        GlousData.Instance.AddGlouToVillage(new Glou(0.6f, 0.12f), 4);
        GlousData.Instance.AddGlouToVillage(new Glou(0.7f, 0.11f), 4);
        GlousData.Instance.AddGlouToVillage(new Glou(0.8f, 0.18f), 5);
        GlousData.Instance.AddGlouToVillage(new Glou(0.9f, 0.18f), 3);
        GlousData.Instance.AddGlouToVillage(new Glou(1, 0.18f), 0);
        Debug.Log("Glous in selector : " + GlousData.Instance.GetGlousInVillage().Count);

    }
}
