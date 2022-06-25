using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCircle : MonoBehaviour
{


    // Start is called before the first frame update
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Glou") return;
        if (other.attachedRigidbody.isKinematic == true) return;
        // kill glou
        //Debug.Log("Kill Glou");
        //other.gameObject.GetComponent<GlouInGame>().KillGlou();


    }


}
