using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Movement_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * -300);
    }

    public IEnumerator Movement_Coroutine()
    {
        while(true)
        {
            for (int i = 0; i < 30; i++)
            {
                this.transform.eulerAngles += new Vector3(0, 0, 0.1f);
                yield return new WaitForSeconds(0.001f);

            }
            for (int i = 0; i < 30; i++)
            {
                this.transform.eulerAngles -= new Vector3(0, 0, 0.1f);
                yield return new WaitForSeconds(0.001f);

            }
        }
        
    }
}
