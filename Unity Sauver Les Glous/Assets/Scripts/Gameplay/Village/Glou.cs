using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glou : MonoBehaviour
{
    private float m_hue;

    public float GetHue()
    {
        return m_hue;
    }

    public void SetHue(float hue)
    {
        m_hue = hue;
    }
}
