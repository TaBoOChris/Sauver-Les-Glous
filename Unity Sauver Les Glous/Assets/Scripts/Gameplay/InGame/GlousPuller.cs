using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousPuller : MonoBehaviour
{
    public float pullRadius = 5;
    public float pullForce = 10;

    public void FixedUpdate()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, pullRadius) ){

            if(collider.transform.parent.tag != "Glou") { return; }

            // calcul de la direction pour attirer le glou
            Vector2 forceDirection = transform.position - collider.gameObject.transform.position;

            // application de la force
            if (collider.GetComponentInParent<Rigidbody2D>())
            {
                Debug.Log("GLOUS PULLER : Aspiration de " + collider.gameObject.name);

                collider.GetComponentInParent<Rigidbody2D>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
