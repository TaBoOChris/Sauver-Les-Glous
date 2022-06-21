using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouCreator : MonoBehaviour
{
    [SerializeField] private int m_nbGlouFusion = 2;

    [SerializeField] private GlouCreatorJar m_jar1;
    [SerializeField] private GlouCreatorJar m_jar2;
    [SerializeField] private GlousSpawner m_spawner;

    private CercleGloumatique m_glouFusion = new CercleGloumatique();

    void FixedUpdate()
    {
        if (m_jar1.getGlou() != null && m_jar2.getGlou() != null)
        {
            // create new glou
            Glou babyGlou = m_glouFusion.Fusion(m_jar1.getGlou().GetComponent<GlouInGame>().GetGlou(), m_jar2.getGlou().GetComponent<GlouInGame>().GetGlou());

            if (babyGlou != null)
            {
                for (int i=0; i< m_nbGlouFusion; ++i)
                {
                    m_spawner.SpawnGlou(babyGlou);
                    if (GameManager.Instance)
                    {
                        GameManager.Instance.AddGlou();
                    }
                }
            }

            // reset jars
            m_jar1.reset();
            m_jar2.reset();

            // reverse drum rotation
            if (LevelProperties.Instance)
            {
                LevelProperties.Instance.SetRotationSpeed(- LevelProperties.Instance.rotationSpeed);
            }
        }
    }
}
