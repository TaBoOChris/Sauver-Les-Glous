using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Glou" && collision.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            Debug.Log("GEAR : kill glou !");
            collision.gameObject.GetComponent<GlouInGame>().KillGlou();
        }
    }
}
