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

    bool m_glouInPipe = false;

    private GameObject m_curGlou = null;

    private float curX = 0.0f;
    private float destX = 10.0f;

    public float dSpeed = 3.0f;

    private void Start()
    {
        m_fakeGlou.SetActive(false);
    }

    void SendGlou(GameObject obj)
    {
        destX = Mathf.Abs(m_glouArrivePosition.position.x - m_glouDetectorInCage.transform.position.x);
        m_curGlou = obj;

        curX = 0.0f;
        m_curGlou.GetComponent<Rigidbody2D>().isKinematic = true;
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
                if (curX >= destX)
                {
                    // Reactivate and teleport glou
                    m_curGlou.SetActive(true);
                    m_curGlou.transform.position = m_glouArrivePosition.position;
                    m_curGlou.transform.rotation = Quaternion.identity;

                    m_glousIntroducer.setGlouToIntroduce(m_curGlou.transform);

                    //Free reference
                    m_curGlou = null;
                    //Hide fake glou
                    m_fakeGlou.SetActive(false);


                    m_glouInPipe = false;
                    return;
                }
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
        else //can Receive
        {
            List<Collider2D> results = new List<Collider2D>();
            if(Physics2D.OverlapCollider(m_glouDetectorInCage, new ContactFilter2D(), results) > 0)
            {
                foreach(var r in results){
                    if(r.tag == "Glou")
                    {
                        canReceiveGlou = false;
                        if(m_glousPuller != null)
                            m_glousPuller.StopPull();
                        SendGlou(r.gameObject);
                    }
                }
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
