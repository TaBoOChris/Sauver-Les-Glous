using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousPuller : MonoBehaviour
{
    public float pullRadius = 2;
    public float pullForce = 50;
    private bool m_isPulling = true;
    [SerializeField] SpriteRenderer m_spriteToColor;
    [SerializeField] bool m_pullOnlyWaiting = false;

    public void Start()
    {
        StartPull();
    }

    public void FixedUpdate()
    {

        Collider2D[] detectedCollider = Physics2D.OverlapCircleAll(transform.position, pullRadius);
                
        foreach (Collider2D collider in detectedCollider)
        {

            if (collider.tag != "Glou") { continue; }
            if (m_pullOnlyWaiting && collider.GetComponent<GlouInGame>().GetState() != GlouInGame.State.Waiting) return;

            // calcul de la direction pour attirer le glou
            Vector2 forceDirection = transform.position - collider.gameObject.transform.position;

            if(!m_isPulling) { forceDirection = -forceDirection; }

            // application de la force
            if (collider.GetComponentInParent<Rigidbody2D>())
            {
                collider.GetComponentInParent<Rigidbody2D>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }

        }
    }

    public void StartPull() { 
        m_isPulling = true;
        if(m_spriteToColor != null)
            m_spriteToColor.color = Color.green;
    }
    public void StopPull()  { 
        m_isPulling = false;
        if (m_spriteToColor != null)
            m_spriteToColor.color = Color.red;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
