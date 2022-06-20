using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionPipe : MonoBehaviour
{

    private Vector2 m_waitingPosition = new Vector2(0, 20);
    [SerializeField] private GlousIntroducer m_glousIntroducer;
    [SerializeField] private GlousPuller m_glousPuller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TP le glou pour le preparer a etre introduit
        if (collision.tag == "Glou")
        {
            collision.transform.position = m_waitingPosition;

            m_glousIntroducer.setGlouToIntroduce(collision.gameObject);
            m_glousPuller.StopPull();
        }
    }

}
