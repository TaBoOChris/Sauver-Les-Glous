using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInGame : MonoBehaviour
{
    private Glou m_glou = null;

    public void SetGlou(Glou glou)
    {
        m_glou = glou;
    }

    public Glou GetGlou()
    {
        return m_glou;
    }
}
