using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlouInGame : MonoBehaviour
{
    private Glou m_glou = null;
    private bool m_isAlive = true;

    [SerializeField] private GameObject m_glouGhost;
    GameObject m_myGhost;

    public enum State
    {
        Waiting,
        InDrum,
        InFusion,
        Saved,
        Dead
    }

    [SerializeField] private State state = State.Waiting;

    public void SetGlou(Glou glou)
    {
        m_glou = glou;
    }

    public Glou GetGlou()
    {
        return m_glou;
    }

    public void KillGlou(bool ghost=true)
    {
        if (state != State.InDrum && state != State.InFusion)
            return;

        m_isAlive = false;
        state = State.Dead;

        if (GameManager.Instance != null)
            GameManager.Instance.GlouDie();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGlouDie();

        // effects of death
        if (ghost)
            StartCoroutine(GlouDieAnimation_Coroutine());
        else
        {
            Destroy(gameObject);
        }

    }

    public bool IsAlive()
    {
        return m_isAlive;
    }


    IEnumerator GlouDieAnimation_Coroutine()
    {
        m_myGhost = Instantiate(
            m_glouGhost, 
            transform.position + new Vector3(0f, 0.5f, 0f), 
            Quaternion.identity
            );

        float ghostScale = Mathf.Clamp(transform.localScale.x, 0.65f, 2.0f);
        m_myGhost.transform.localScale = new Vector3(ghostScale, ghostScale, 1f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1.0f);

        Destroy(m_myGhost);
        Destroy(gameObject);
        yield return null;
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

    public void SetState(State state)
    {
        this.state = state;
    }

    public State GetState()
    {
        return state;
    }
}
