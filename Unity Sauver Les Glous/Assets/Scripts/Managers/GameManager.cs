using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private int m_nbGlousStartLevel = 10;
	private int m_nbGlousAlive;

	[SerializeField] private GlousSpawner m_glousSpawner;

	[Header("Timer")]
	[SerializeField] private Timer m_timer;
	[SerializeField] private int m_gameTime = 60;

	[Header("End menu")]
	[SerializeField] private GameObject m_endMenu;
	[SerializeField] private TextMeshProUGUI m_endMenuText;

	[Header("Pause")]
	[SerializeField] private GameObject m_pauseMenu;
	private static bool m_isGamePaused = false;
	private InputActions m_inputActions;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scene");
			return;
		}

		Instance = this;

		m_inputActions = new InputActions();
		m_inputActions.Game.Pause.performed += context => PauseKeyPressed();
	}

	void Start()
    {
		StartGame();
		if(CursorManager.Instance != null)
			CursorManager.Instance.SetPointer();
	}

	public void StartGame()
    {
		Time.timeScale = 1;
		m_isGamePaused = false;

		m_endMenu.SetActive(false);
		m_nbGlousAlive = 0;
		m_glousSpawner.SpawnGlous(m_nbGlousStartLevel);	//SpawnGlous();
		//SpawnPlaterform();
		//Rotation();
		m_timer.StartTimer(m_gameTime);	//StartTimer();
    }

	public void EndGame()
    {
		if (CursorManager.Instance != null)
			CursorManager.Instance.SetPointer();
		if (m_nbGlousAlive <= 0)
        {
			m_endMenuText.text = "Tu n'as pas réussi à sauver les Glous ...";
        }
        else
        {
			m_endMenuText.text = "La machine est enfin arretée !\nTu as sauvé <color=#86E989>" + m_nbGlousAlive + "</color> Glous.  Bien joué !";
        }

		// update players glous
		List<Glou> survivorGlousList = new List<Glou>();
		Transform glousParentGO = m_glousSpawner.GetGlousParentGO().transform;
		foreach (Transform child in glousParentGO)
        {
			Glou survivorGlou = child.GetComponent<GlouInGame>().GetGlou();
			if (survivorGlou != null)
            {
				survivorGlousList.Add(survivorGlou);
				Debug.Log("SurviverGlou Hue : " + survivorGlou.hue);
            }
        }

		// pop up end menu
		m_endMenu.SetActive(true);
		Time.timeScale = 0f;
		m_isGamePaused = true;
		// must not be able to pause/unpause when in endMenu
		m_inputActions.Game.Disable();
	}

	public void AddGlou()
    {
		m_nbGlousAlive++;
    }

	public void GlouDie()
    {
		m_nbGlousAlive--;
		if(m_nbGlousAlive <= 0)
        {
			EndGame();
        }
    }

	public int GetNbGlousAlive()
    {
		return m_nbGlousAlive;
    }

	public void PauseKeyPressed()
    {
		if (m_isGamePaused)
		{
			ResumeGame();
		}
		else
		{
			PauseGame();
		}
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;
		m_isGamePaused = true;
		m_pauseMenu.SetActive(true);
		Debug.Log("Game Paused");

		if (CheatCodes.Instance != null)
        {
			CheatCodes.Instance.Disable();
		}
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
		m_isGamePaused = false;
		m_pauseMenu.SetActive(false);
		Debug.Log("Game Resumed");

		if (CheatCodes.Instance != null)
		{
			CheatCodes.Instance.Enable();
		}
	}

	public bool IsGamePaused()
    {
		return m_isGamePaused;
    }

	private void OnEnable()
	{
		m_inputActions.Game.Enable();
	}

	private void OnDisable()
	{
		m_inputActions.Game.Disable();
	}
}

