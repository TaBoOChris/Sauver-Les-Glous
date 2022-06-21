using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousIntroducer : MonoBehaviour
{

    GameObject m_glouToIntroduce;
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
        if(m_glouToIntroduce == null)
        {
            Debug.Log("INTRODUCER : NO GLOU TO INTRODUCE");
            return;
        }

        m_glouToIntroduce.transform.position = transform.position;
        m_glouToIntroduce.transform.rotation = Quaternion.identity;
        m_glouToIntroduce.GetComponent<Rigidbody2D>().isKinematic = false;
        m_glouToIntroduce.GetComponent<GlouInGame>().UpdateState();
        m_glouToIntroduce = null;

        if(m_pipeTransferer != null)
            m_pipeTransferer.canReceiveGlou = true;
        if(m_glousPuller != null)
             m_glousPuller.StartPull();
    }


    public void setGlouToIntroduce(GameObject glouToIntroduce)
    {
        m_glouToIntroduce = glouToIntroduce;

        m_glouToIntroduce.GetComponent<Rigidbody2D>().isKinematic = true;
        m_glouToIntroduce.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (m_AutoIntroduce)
        {
            IntroduceGlou();
        }
    }

}
