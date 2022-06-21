using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouCreatorJar : MonoBehaviour
{
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Transform m_glouInJarTrans;
    [SerializeField] private GlousPuller m_glousPuller;

    private GameObject m_curGlou = null;
    private float newAspirationDelay = 2f;
    private bool CanCatchGlou = true;

    private void FixedUpdate()
    {
        if (!CanFusionGlous()) { return; }

        if (!CanCatchGlou) { return; }

        if (m_curGlou == null)
        {
            List<Collider2D> results = new List<Collider2D>();
            if (Physics2D.OverlapCollider(m_collider, new ContactFilter2D(), results) > 0)
            {
                foreach (var r in results)
                {
                    if (r.tag == "Glou")
                    {
                        if (r.gameObject.GetComponent<GlouInGame>().GetState() == GlouInGame.State.InDrum)
                        {
                            m_glousPuller.enabled = false;
                            m_curGlou = r.gameObject;

                            m_curGlou.GetComponent<GlouInGame>().SetState(GlouInGame.State.InFusion);

                            Debug.Log("glou caught in creator jar");
                            m_curGlou.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            m_curGlou.GetComponent<Rigidbody2D>().isKinematic = true;
                            m_curGlou.transform.rotation = Quaternion.identity;
                            m_curGlou.transform.position = m_glouInJarTrans.position;

                            m_curGlou.transform.SetParent(m_glousPuller.transform);

                            StartCoroutine(ReleaseGlou());
                        }
                    }
                }
            }
        }
    }

    public void reset()
    {
        Destroy(m_curGlou);
        m_glousPuller.enabled = true;
    }

    public GameObject getGlou()
    {
        return m_curGlou;
    }

    public bool CanFusionGlous()
    {
        bool tmp = true;

        if (GameManager.Instance)
            tmp = GameManager.Instance.GetNbGlousAlive() >= 2;


        return tmp;
    }

    IEnumerator ReleaseGlou()
    {
        // on attend 10 sec pour jeter le glou
        yield return new WaitForSeconds(10f);

        if(m_curGlou == null) { yield return null; }

        CanCatchGlou = false;
        m_glousPuller.enabled = true;

        m_curGlou.GetComponent<GlouInGame>().SetState(GlouInGame.State.InDrum);

        Debug.Log("CREATOR JAR : glou Drop in Game");
        //m_curGlou.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_curGlou.GetComponent<Rigidbody2D>().isKinematic = false;
        m_curGlou.transform.rotation = Quaternion.identity;
        m_curGlou.transform.position = m_collider.transform.position;

        m_curGlou.transform.SetParent(m_glousPuller.transform);
        m_curGlou.GetComponent<Rigidbody2D>().AddForce(-transform.position.normalized * 5);
        m_curGlou = null;


        // On attend 2sec pour pouvoir attraper de nouveau un glou 
        yield return new WaitForSeconds(2f);
        CanCatchGlou = true;
    }
}
