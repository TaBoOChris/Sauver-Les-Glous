using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexManager : AbstractSingleton<GloudexManager>
{
    // Liste des Glous que l'on vient juste de découvir et qui sont à inscrire dans le Gloudex
    [SerializeField] private List<Glou.SkinGlou> m_glousDecouverts = new List<Glou.SkinGlou>();
    // Liste des Glous qu'on a inscrit dans le Gloudex
    [SerializeField] private List<Glou.SkinGlou> m_glousInGloudex = new List<Glou.SkinGlou>();

    private int m_maxGlous = 12;
    [SerializeField] private bool m_isProfGlouglouDecouvert;
    [SerializeField] private bool m_isProfGlouglouInGloudex;

    protected override void Awake()
    {
        /*m_glousInGloudex.Add(Glou.SkinGlou.Rouge);
        m_glousInGloudex.Add(Glou.SkinGlou.Bleu);
        m_glousInGloudex.Add(Glou.SkinGlou.Jaune);*/

        base.Awake();
    }

    // Permet de voir si un Glou est dans le Gloudex
    public bool IsInGloudex(Glou.SkinGlou skinTarget)
    {
        foreach(Glou.SkinGlou skin in m_glousInGloudex)
        {
            if (skinTarget == skin) return true;
        }
        return false;
    }

    // Permet de voir si un Glou est dans la fille d'attente
    public bool IsInGloudexQueue(Glou.SkinGlou skinTarget)
    {
        foreach (Glou.SkinGlou skin in m_glousDecouverts)
        {
            if (skinTarget == skin) return true;
        }
        return false;
    }

    // Permet de voir si un Glou vient d'être découvert
    public bool IsWaitingToEnterGloudex(Glou.SkinGlou skinTarget)
    {
        foreach (Glou.SkinGlou skin in m_glousDecouverts)
        {
            if (skinTarget == skin) return true;
        }
        return false;
    }

    // Permet d'ajouter un Glou au Gloudex
    public void AddGlouInGloudex(Glou.SkinGlou skin)
    {
        m_glousInGloudex.Add(skin);
        m_glousDecouverts.Remove(skin);

        // Si tous les Glous sont dans le Gloudex, on découvre le professeur Glouglou
        if(m_glousInGloudex.Count == m_maxGlous)
        {
            m_isProfGlouglouDecouvert = true;
        }
    }

    // Permet d'ajouter un Glou à la liste d'attente
    public void AddGlouInWaintingQueue(Glou.SkinGlou skin)
    {
        m_glousDecouverts.Add(skin);
    }

    // Cherches les nouveaux Glous
    public void DetectNewGlou()
    {
        foreach (Glou glou in GlousData.Instance.GetGlousInSelector())
        {
            if (!IsInGloudex(glou.skin) && !IsInGloudexQueue(glou.skin))
            {
                AddGlouInWaintingQueue(glou.skin);
            }
        }
        
    }

    public int GetGlouDecouvertsSize()
    {
        return m_glousDecouverts.Count;
    }

    public bool getIsProfesseurGlouglouDecouvert()
    {
        return m_isProfGlouglouDecouvert;
    }

    public bool getIsProfesseurGlouglouInGloudex()
    {
        return m_isProfGlouglouInGloudex;
    }

    public void setIsProfesseurGlouglouInGloudex(bool b)
    {
        m_isProfGlouglouInGloudex = b;
    }
}
