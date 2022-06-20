using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInGame : MonoBehaviour
{
    private Glou m_glou = null;
    private bool m_isAlive = true;

    [SerializeField] private GameObject m_glouGhost;

    public void SetGlou(Glou glou)
    {
        m_glou = glou;
    }

    public Glou GetGlou()
    {
        return m_glou;
    }

    public void KillGlou()
    {
        m_isAlive = false;

        // effects of death
        StartCoroutine(GlouDieAnimation_Coroutine());

        if (GameManager.Instance !=null)
            GameManager.Instance.GlouDie();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGlouDie();

        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return m_isAlive;
    }


    IEnumerator GlouDieAnimation_Coroutine()
    {
        GameObject newGlou = Instantiate(
            m_glouGhost, 
            transform.position + new Vector3(0f, 0.5f, 0f), 
            Quaternion.identity
            );

        newGlou.transform.localScale = transform.localScale;
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(newGlou);
    }
}
