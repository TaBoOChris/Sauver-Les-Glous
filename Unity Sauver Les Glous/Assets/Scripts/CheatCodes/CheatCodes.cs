using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodes : MonoBehaviour
{
    private InputActions m_inputActions;

    [SerializeField] private Timer m_timer;
    [SerializeField] private GlousSpawner m_glouSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        m_inputActions = new InputActions();

        m_inputActions.CheatCodes.AddTime.performed += context => AddTime();
        m_inputActions.CheatCodes.EndTimer.performed += context => EndTimer();
        m_inputActions.CheatCodes.SpawnGlou.performed += context => SpawnGlou();
    }

    public void AddTime()
    {
        m_timer.AddTimeSeconds(60);
        Debug.Log("<CHEAT> AddTime");
    }

    public void EndTimer()
    {
        m_timer.EndTimer();
        Debug.Log("<CHEAT> EndTimer");
    }

    public void SpawnGlou()
    {
        m_glouSpawner.SpawnGlous(1);
        Debug.Log("<CHEAT> SpawnGlou");
    }

    private void OnEnable()
    {
        m_inputActions.CheatCodes.Enable();
    }

    private void OnDisable()
    {
        m_inputActions.CheatCodes.Disable();
    }
}
