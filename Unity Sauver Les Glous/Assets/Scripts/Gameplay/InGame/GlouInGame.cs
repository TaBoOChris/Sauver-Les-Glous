using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInGame : MonoBehaviour
{
    private Glou m_glou = null;
    private bool m_isAlive = true;

    [SerializeField] private GameObject m_glouGhost;


    public enum State
    {
        Waiting,
        InDrum,
        Saved,
        Dead
    }

    private State state = State.Waiting;

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
        state = State.Dead;

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

    public void UpdateState()
    {
        switch (state)
        {
            case State.Waiting:
                state = State.InDrum;
                break;

            case State.InDrum:
                state = State.Saved;
                if (GameManager.Instance)
                    GameManager.Instance.AddGlouSaved();
                break;
            case State.Saved:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }


}
