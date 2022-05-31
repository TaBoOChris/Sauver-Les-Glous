using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousData : AbstractSingleton<GlousData>
{
    private List<Glou> m_glousInVillage = new List<Glou>(); 
    private List<Glou> m_glousInSelector = new List<Glou>();
    
    public List<Glou> GetGlousInVillage()
    {
        return m_glousInVillage;
    }

    public List<Glou> GetGlousInSelector()
    {
        return m_glousInSelector;
    }

    public void AddGlouToVillage(Glou glou, int houseNum)
    {
        glou.houseID = houseNum;
        m_glousInVillage.Add(glou);
    }

    public void MoveGlouToSelector(ref Glou glou)
    {
        Glou g = glou;
        m_glousInVillage.Remove(glou);
        m_glousInSelector.Add(g);
    }

    public void MoveGlouToVillage(ref Glou glou)
    {
        Glou g = glou;
        m_glousInSelector.Remove(g);
        m_glousInVillage.Add(glou);
    }

    public void SetGlousInSelector(List<Glou> newList)
    {
        m_glousInSelector = newList;
    }
}
