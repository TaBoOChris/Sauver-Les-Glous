using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : AbstractSingleton<LevelProperties>
{
    [Header("Level properties")]
    public float rotationSpeed = 10f;
    [SerializeField] private float m_maxRotationSpeed = 2f;
    [SerializeField] private float m_minRotationSpeed = 0.7f;

    private bool m_isSpeedUp = false;

    public void SpeedUpDown()
    {
        if (!m_isSpeedUp)
        {
            if(rotationSpeed <= m_minRotationSpeed && rotationSpeed >= 0)
            {
                rotationSpeed = -m_minRotationSpeed;
            }
            else
            {
                rotationSpeed -= 0.2f;
            }
            if (rotationSpeed <= -m_maxRotationSpeed)
            {
                m_isSpeedUp = !m_isSpeedUp;
            }
        }
    else
        {
            if (rotationSpeed >= -m_minRotationSpeed && rotationSpeed <= 0)
            {
                rotationSpeed = m_minRotationSpeed;
            }
            else
            {
                rotationSpeed += 0.2f;
            }
            if (rotationSpeed >= m_maxRotationSpeed)
            {
                m_isSpeedUp = !m_isSpeedUp;
            }
        }

    }

    public void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

}
