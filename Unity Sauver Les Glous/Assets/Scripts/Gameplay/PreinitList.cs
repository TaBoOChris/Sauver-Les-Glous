using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreinitList : MonoBehaviour
{
    [SerializeField] List<Glou> m_glousStartingList = new List<Glou> {
        new Glou(Glou.SkinGlou.Rouge, 0.8f), new Glou(Glou.SkinGlou.Bleu, 1f), new Glou(Glou.SkinGlou.Jaune, 1.2f),
        new Glou(Glou.SkinGlou.Rouge, 0.8f), new Glou(Glou.SkinGlou.Bleu, 1f), new Glou(Glou.SkinGlou.Jaune, 1.2f),
        new Glou(Glou.SkinGlou.Rouge, 0.8f), new Glou(Glou.SkinGlou.Bleu, 1f), new Glou(Glou.SkinGlou.Jaune, 1.2f),
        

    };
    // Start is called before the first frame update
    void Start()
    {
        GlousData.Instance.SetGlousInSelector(m_glousStartingList);
        Debug.Log("Glous in selector : " + GlousData.Instance.GetGlousInSelector().Count);


    }
}
