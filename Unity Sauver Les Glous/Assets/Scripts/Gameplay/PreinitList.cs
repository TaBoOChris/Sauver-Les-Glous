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
    }
}
