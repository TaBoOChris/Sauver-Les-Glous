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
    [SerializeField] private GlousSpawner m_glouSpawnerCheat;

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

        //m_inputActions.CheatCodes.AddTime.performed += context => AddTime();
        //m_inputActions.CheatCodes.EndTimer.performed += context => EndTimer();
        m_inputActions.CheatCodes.SpawnGlou.performed += context => SpawnGlou(); // glou will not be in the village after the game
        m_inputActions.CheatCodes.SpawnGlouHue.performed += context => SpawnNewGlou(); // glou will be in the village after the game
        m_inputActions.CheatCodes.ReverseRotation.performed += context => ReverseRotation();
        m_inputActions.CheatCodes.EndGame.performed += context => EndGame();
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

    public void EndGame()
    {
        GameManager.Instance.EndGame();
        Debug.Log("<CHEAT> EndGame");
    }

    public void SpawnGlou()
    {
        m_glouSpawnerCheat.SpawnGlou();
        GameManager.Instance.AddGlou();
        Debug.Log("<CHEAT> SpawnGlou");
    }

    public void SpawnNewGlou()
    {
        m_glouSpawner.SpawnNewGlou();
        GameManager.Instance.AddGlou();
        Debug.Log("<CHEAT> SpawnNewGlou");
    }

    public void ReverseRotation()
    {
        LevelProperties.Instance.SetRotationSpeed(-LevelProperties.Instance.rotationSpeed);
        Debug.Log("<CHEAT> ReverseRotation ");
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
