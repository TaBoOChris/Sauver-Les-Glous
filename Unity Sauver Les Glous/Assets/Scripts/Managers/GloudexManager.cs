using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexManager : AbstractSingleton<GloudexManager>
{
    // Liste des Glous que l'on vient juste de d�couvir et qui sont � inscrire dans le Gloudex
    [SerializeField] private List<Glou.SkinGlou> m_glousDecouverts = new List<Glou.SkinGlou>();
    // Liste des Glous qu'on a inscrit dans le Gloudex
    [SerializeField] private List<Glou.SkinGlou> m_glousInGloudex = new List<Glou.SkinGlou>();

    [SerializeField] private SpriteRenderer m_notification;

    protected override void Awake()
    {
        /*m_glousInGloudex.Add(Glou.SkinGlou.Rouge);
        m_glousInGloudex.Add(Glou.SkinGlou.Bleu);
        m_glousInGloudex.Add(Glou.SkinGlou.Jaune);*/

        base.Awake();
    }

    private void Start()
    {
        if (m_glousDecouverts.Count > 0)
        {
            m_notification.enabled = true;
        }
        else m_notification.enabled = false;
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

    // Permet de voir si un Glou vient d'�tre d�couvert
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

        if(m_glousDecouverts.Count == 0)
            m_notification.enabled = false;
    }
}
