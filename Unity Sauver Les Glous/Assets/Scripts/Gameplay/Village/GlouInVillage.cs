using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInVillage : MonoBehaviour
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInVillage : MonoBehaviour
{
    private float m_hue;
    private int m_houseID;
    private float m_size;

    [SerializeField] private SpriteRenderer m_bodyRenderer;
    [SerializeField] private SpriteRenderer m_expressionRenderer;

    public SpriteRenderer GetBodyRenderer()
    {
        return m_bodyRenderer;
    }

    public SpriteRenderer GetExpressionRenderer()
    {
        return m_expressionRenderer;
    }

    public float GetHue()
    {
        return m_hue;
    }

    public void SetHue(float hue)
    {
        m_hue = hue;
    }

    public int GetHouseID()
    {
        return m_houseID;
    }

    public void SetHouseID(int id)
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
