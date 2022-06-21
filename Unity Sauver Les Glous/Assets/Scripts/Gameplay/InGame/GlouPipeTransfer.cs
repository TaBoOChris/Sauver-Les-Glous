using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Lorsque Actif (needs public bool)
 * Si contact avec zone
 * prendre premier glou en contact
 * Et l'envoyer
*/

public class GlouPipeTransfer : MonoBehaviour
{
    [SerializeField] Collider2D m_glouDetectorInCage;
    [SerializeField] Transform m_glouArrivePosition;
    [SerializeField] GameObject m_fakeGlou;

    [Header("Puller and introducer")]
    [SerializeField] GlousPuller m_glousPuller = null;
    [SerializeField] GlousIntroducer m_glousIntroducer;
    [SerializeField] bool m_negativeFunction = false;
    public bool canReceiveGlou = false;

    [SerializeField] Animator m_toggleAnim;

    bool m_glouInPipe = false;

    private GameObject m_curGlou = null;

    private float curX = 0.0f;
    private float destX = 10.0f;

    public float dSpeed = 3.0f;

    private bool m_bCanSendGlou;

    private void Start()
    {
        m_fakeGlou.SetActive(false);
    }

    void SendGlou()
    {
        if (m_toggleAnim != null)
        {
            m_toggleAnim.enabled = true;
            m_toggleAnim.speed = dSpeed;
        }
        destX = Mathf.Abs(m_glouArrivePosition.position.x - m_glouDetectorInCage.transform.position.x);

        curX = 0.0f;
        if (m_negativeFunction)
            curX = destX;

        m_curGlou.SetActive(false); //Hide the chosen glou

        //Display the fake glou (pipeSphere)
        m_fakeGlou.SetActive(true);

        m_fakeGlou.transform.position = m_glouDetectorInCage.transform.position;

        m_glouInPipe = true;
    }

    float ForwardFunction(float x)
    {
        return x - 1.2f * Mathf.Exp(x / 4f) + 1.6f * Mathf.Log(2 + x);
    }

    private void FixedUpdate()
    {
        if (!canReceiveGlou){  //Cannot receive; update if glou in pipe
            if (m_glouInPipe)
            {
                if ((!m_negativeFunction && curX >= destX) || (m_negativeFunction && curX <= 0.1f))
                {

                    if (m_toggleAnim != null)
                    {
                        m_toggleAnim.speed = 0f;
                        m_toggleAnim.enabled = false;
                    }

                    // Reactivate and teleport glou
                    m_curGlou.SetActive(true);
                    m_curGlou.transform.position = m_glouArrivePosition.position;
                    m_curGlou.transform.rotation = Quaternion.identity;

                    m_glousIntroducer.setGlouToIntroduce(m_curGlou);

                    //Free reference
                    m_curGlou = null;
                    //Hide fake glou
                    m_fakeGlou.SetActive(false);

                    m_glouInPipe = false;
                    return;
                }
                if (m_negativeFunction)
                    curX -= Time.deltaTime * dSpeed*1.5f;
                else
                    curX += Time.deltaTime * dSpeed;
                float x = curX;
                float y = ForwardFunction(curX);
                Vector3 next;
                if (m_negativeFunction)
                {
                    next = m_glouArrivePosition.transform.position + new Vector3(x, -y, 0);
                }
                else
                {
                    next = m_glouDetectorInCage.transform.position + new Vector3(x, y, 0);
                }
                m_fakeGlou.transform.position = next;
            }
        }
        else if(m_curGlou==null) //can Receive
        {
            List<Collider2D> results = new List<Collider2D>();
            if(Physics2D.OverlapCollider(m_glouDetectorInCage, new ContactFilter2D(), results) > 0)
            {
                foreach(var r in results){
                    if (r.tag == "Glou")
                    {
                        if (r.gameObject.GetComponent<GlouInGame>().GetState() == GlouInGame.State.InDrum || r.gameObject.GetComponent<GlouInGame>().GetState() == GlouInGame.State.Waiting)
                        {
                            // Si c'est en bas et que le detecteur est bien placé, ou si c'est en haut 
                            if ((m_negativeFunction && m_bCanSendGlou) || !m_negativeFunction){

                                canReceiveGlou = false;
                                if (m_glousPuller != null && !m_negativeFunction)
                                    m_glousPuller.StopPull();
                                m_curGlou = r.gameObject;

                                SendGlou();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (m_negativeFunction && collision.tag == "SpawnerTrigger")
            m_bCanSendGlou = true;

        if (m_negativeFunction && collision.tag == "AttractorTrigger")
        {
            if (m_glousPuller != null)
            {
                m_glousPuller.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (m_negativeFunction && collision.tag == "SpawnerTrigger")
            m_bCanSendGlou = false;

        if (m_negativeFunction && collision.tag == "AttractorTrigger")
        {
            if (m_glousPuller != null)
            {
                m_glousPuller.enabled = false;
            }
        }

    }



    private void OnDrawGizmos()
    { 
        float dest_x_gizmo = Mathf.Abs(m_glouArrivePosition.position.x - m_glouDetectorInCage.transform.position.x);

        int div = 20;
        float inc = dest_x_gizmo / (float)div;
        float cur_x_gizmo = 0.0f;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_glouArrivePosition.position, 0.2f);

        Vector3 last = m_glouDetectorInCage.transform.position;
        if (m_negativeFunction)
        {
            last = m_glouArrivePosition.position;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_glouDetectorInCage.transform.position, 0.2f);

        for (int i=0; i<div; i++)
        {
            cur_x_gizmo += inc;
            float y = ForwardFunction(cur_x_gizmo);
            float x = cur_x_gizmo;
            Vector3 next;
            if (m_negativeFunction)
            {
                next = m_glouArrivePosition.transform.position + new Vector3(x, -y, 0);
            }
            else
            {
                next = m_glouDetectorInCage.transform.position + new Vector3(x, y, 0);
            }
            
            Gizmos.DrawLine(last, next);
            last = next;
        }
    }
}
