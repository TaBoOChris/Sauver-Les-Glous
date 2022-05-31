using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInGame : MonoBehaviour
{
    private Glou m_glou = null;
    private bool m_isAlive = true;

    public void SetGlou(Glou glou)
    {
        m_glou = glou;
    }

    public Glou GetGlou()
    {
        return m_glou;
    }

    public void KillGlou()
    {
        m_isAlive = false;
    }

    public bool IsAlive()
    {
        return m_isAlive;
    }
}
