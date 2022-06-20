using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloudexManager : MonoBehaviour
{
    [SerializeField] private List<Glou.SkinGlou> _glousDecouverts = new List<Glou.SkinGlou>();
    [SerializeField] private List<Glou.SkinGlou> _glousInGloudex = new List<Glou.SkinGlou>();

    [SerializeField] private SpriteRenderer _notification;

    public static GloudexManager Instance { get; private set; }

    private void Awake()
    {
        /*glousDecouverts.Add(Glou.SkinGlou.Rouge);
        glousDecouverts.Add(Glou.SkinGlou.Jaune);
        glousDecouverts.Add(Glou.SkinGlou.Bleu);*/

        /*glousInGloudex.Add(Glou.SkinGlou.Rouge);
        glousInGloudex.Add(Glou.SkinGlou.Jaune);
        glousInGloudex.Add(Glou.SkinGlou.Bleu);*/

        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scene");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (_glousDecouverts.Count > 0)
        {
            _notification.enabled = true;
        }
        else _notification.enabled = false;
    }

    public bool IsInGloudex(Glou.SkinGlou skinTarget)
    {
        foreach(Glou.SkinGlou skin in _glousInGloudex)
        {
            if (skinTarget == skin) return true;
        }
        return false;
    }

    public bool IsWaitingToEnterGloudex(Glou.SkinGlou skinTarget)
    {
        foreach (Glou.SkinGlou skin in _glousDecouverts)
        {
            if (skinTarget == skin) return true;
        }
        return false;
    }

    public void AddGlouInGloudex(Glou.SkinGlou skin)
    {
        _glousInGloudex.Add(skin);
        _glousDecouverts.Remove(skin);

        if(_glousDecouverts.Count == 0)
            _notification.enabled = false;
    }
}
