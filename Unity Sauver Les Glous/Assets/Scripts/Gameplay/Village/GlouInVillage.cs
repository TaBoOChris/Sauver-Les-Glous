using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInVillage : MonoBehaviour
{
    private Glou m_glou = null;

    private int? m_houseID;
    private float m_size;
    private Glou.SkinGlou m_skin;

    [SerializeField] private SpriteRenderer m_bodyRenderer;
    [SerializeField] private SpriteRenderer m_expressionRenderer;

    public void SetGlou(Glou glou)
    {
        m_glou = glou;
    }

    public Glou GetGlou()
    {
        return m_glou;
    }


    public SpriteRenderer GetBodyRenderer()
    {
        return m_bodyRenderer;
    }

    public SpriteRenderer GetExpressionRenderer()
    {
        return m_expressionRenderer;
    }

    public Glou.SkinGlou GetSkin()
    {
        return m_skin;
    }

    public void SetSkin(Glou.SkinGlou skin)
    {
        m_skin = skin;
    }

    public int? GetHouseID()
    {
        return m_houseID;
    }

    public void SetHouseID(int? id)
    {
        m_houseID = id;
    }

    public float GetSize()
    {
        return m_size;
    }

    public void SetSize(float size)
    {
        m_size = size;
    }
}
