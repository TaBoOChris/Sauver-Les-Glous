using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouCreator : MonoBehaviour
{

    [SerializeField] private GlouCreatorJar m_jar1;
    [SerializeField] private GlouCreatorJar m_jar2;

    void FixedUpdate()
    {
        if (m_jar1.getGlou() != null && m_jar2.getGlou() != null)
        {
            Debug.Log("CREATE GLOU");
            // create new glou
            // reset jars
            // turn tambour das lautre sens
        }
    }
}
