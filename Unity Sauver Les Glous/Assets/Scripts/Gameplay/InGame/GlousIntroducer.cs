using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousIntroducer : MonoBehaviour
{

    Transform m_glousToIntroduce;
    [SerializeField] GlousPuller m_glousPuller = null;
    [SerializeField] GlouPipeTransfer m_pipeTransferer = null;

    [SerializeField] bool m_AutoIntroduce = false;

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

        if(m_pipeTransferer != null)
            m_pipeTransferer.canReceiveGlou = true;
        if(m_glousPuller != null)
             m_glousPuller.StartPull();
    }


    public void setGlouToIntroduce(Transform glouToIntroduce)
    {
        m_glousToIntroduce = glouToIntroduce;
        m_glousToIntroduce.gameObject.SetActive(true);
        m_glousToIntroduce.position = transform.position;
        m_glousToIntroduce.rotation = Quaternion.identity;
        if (m_AutoIntroduce)
        {
            IntroduceGlou();
        }
    }

}
