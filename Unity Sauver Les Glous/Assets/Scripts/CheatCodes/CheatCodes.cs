using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodes : MonoBehaviour
{
    public static CheatCodes Instance { get; private set; }

    private InputActions m_inputActions;

    [SerializeField] private Timer m_timer;
    [SerializeField] private GlousSpawner m_glouSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of CheatCodes in the scene");
            return;
        }
        Instance = this;

        m_inputActions = new InputActions();

        m_inputActions.CheatCodes.AddTime.performed += context => AddTime();
        m_inputActions.CheatCodes.EndTimer.performed += context => EndTimer();
        m_inputActions.CheatCodes.SpawnGlou.performed += context => SpawnGlou();
        m_inputActions.CheatCodes.SpawnGlouHue.performed += context => SpawnGlouHue();
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
    public void SpawnGlouHue()
    {
        float hue = Random.Range(0f, 1f);
        m_glouSpawner.SpawnGlousHue(hue);
        Debug.Log("<CHEAT> SpawnGlouHue " + hue);
    }

    public void Enable()
    {
        m_inputActions.CheatCodes.Enable();
    }

    public void Disable()
    {
        m_inputActions.CheatCodes.Disable();
    }

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
        Disable();
    }
}
