using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GEAR : Collision !");

        if (collision.gameObject.tag == "Glou")
        {
            Debug.Log("GEAR : kill glou !");
            collision.gameObject.GetComponent<GlouInGame>().KillGlou();
        }
    }
}
