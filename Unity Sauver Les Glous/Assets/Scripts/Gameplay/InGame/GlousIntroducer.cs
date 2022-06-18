using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousIntroducer : MonoBehaviour
{

    Transform m_glousToIntroduce;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Spawn un Glou apres avoir fait un tour complet
        if (collision.tag == "SpawnerTrigger")
        {
            Debug.Log("ROTATION : Spawn Glou (collision with " + collision.gameObject.name + "  )");
            IntroduceGlou();
        }
    }


    private void IntroduceGlou()
    {
        if(m_glousToIntroduce == null)
        {
            Debug.Log("INTRODUCER : NO GLOU TO INTRODUCE");
            return;
        }

        m_glousToIntroduce.position = transform.position;
    }


    public void setGlouToIntroduce(Transform glouToIntroduce)
    {
        m_glousToIntroduce = glouToIntroduce;
    }

}
