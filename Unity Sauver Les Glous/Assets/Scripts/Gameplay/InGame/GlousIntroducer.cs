using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousIntroducer : MonoBehaviour
{

    Transform m_glousToIntroduce;
    [SerializeField] GlousPuller m_glousPuller;
    [SerializeField] GlouPipeTransfer m_pipeTransferer;


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
        m_glousToIntroduce.GetComponent<Rigidbody2D>().isKinematic = false;
        m_glousToIntroduce = null;

        m_pipeTransferer.canReceiveGlou = true;
        m_glousPuller.StartPull();
    }


    public void setGlouToIntroduce(Transform glouToIntroduce)
    {
        m_glousToIntroduce = glouToIntroduce;
    }

}
