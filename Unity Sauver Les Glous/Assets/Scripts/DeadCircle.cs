using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Glou") return;
        
        Debug.Log("Kill Glou");

        if(GameManager.Instance)
            GameManager.Instance.GlouDie();

        Destroy(other.gameObject,0.2f);
    }
}
