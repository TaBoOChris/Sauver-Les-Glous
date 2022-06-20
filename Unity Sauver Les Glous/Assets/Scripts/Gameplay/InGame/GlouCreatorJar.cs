using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouCreatorJar : MonoBehaviour
{
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Transform m_glouInJarTrans;
    [SerializeField] private GlousPuller m_glousPuller;

    private GameObject m_curGlou = null;

    void FixedUpdate()
    {
        if (m_curGlou == null)
        {
            List<Collider2D> results = new List<Collider2D>();
            if (Physics2D.OverlapCollider(m_collider, new ContactFilter2D(), results) > 0)
            {
                foreach (var r in results)
                {
                    if (r.tag == "Glou")
                    {
                        m_glousPuller.enabled = false;
                        m_curGlou = r.gameObject;

                        Debug.Log("glou caught in creator jar");
                        m_curGlou.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        m_curGlou.GetComponent<Rigidbody2D>().isKinematic = true;
                        m_curGlou.transform.rotation = Quaternion.identity;
                        m_curGlou.transform.position = m_glouInJarTrans.position;

                        m_curGlou.transform.SetParent(m_glousPuller.transform);
                    }
                }
            }
        }
    }

    public GameObject getGlou()
    {
        return m_curGlou;
    }
}
