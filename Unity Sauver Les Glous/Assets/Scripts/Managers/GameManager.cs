using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private int _NbGlousStartLevel = 10;
	private int _NbGlousAlive;

	[SerializeField] GlousSpawner glousSpawner;

	[Header("Timer")]
	[SerializeField] Timer timer;
	[SerializeField] private int gameTime = 60;

	[Header("End menu")]
	[SerializeField] GameObject endMenu;
	[SerializeField] TextMeshProUGUI endMenuText;

	[Header("Pause")]
	public static bool m_isGamePaused = false;
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

    void Update()
    {
        
    }

	public void StartGame()
    {
		Time.timeScale = 1;
		m_isGamePaused = false;

		endMenu.SetActive(false);
		_NbGlousAlive = 0;
		glousSpawner.SpawnGlous(_NbGlousStartLevel);	//SpawnGlous();
		//SpawnPlaterform();
		//Rotation();
		timer.StartTimer(gameTime);							//StartTimer();
    }

	public void EndGame()
    {
		if (CursorManager.Instance != null)
			CursorManager.Instance.SetPointer();
		if (_NbGlousAlive <= 0)
        {
			endMenuText.text = "Tu n'as pas réussi à sauver les Glous ...";
        }
        else
        {
			endMenuText.text = "La machine est enfin arretée !\nTu as sauvé <color=#86E989>" + _NbGlousAlive + "</color> Glous.  Bien joué !";
        }

		endMenu.SetActive(true);
		PauseGame();
		// must not be able to pause/unpause when in endMenu
		m_inputActions.Game.Disable();
	}

	public void AddGlou()
    {
		_NbGlousAlive++;
    }

	public void GlouDie()
    {
		_NbGlousAlive--;
		if(_NbGlousAlive <= 0)
        {
			EndGame();
        }
    }

	public int GetNbGlousAlive()
    {
		return _NbGlousAlive;
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

	void PauseGame()
	{
		Time.timeScale = 0f;
		m_isGamePaused = true;
		Debug.Log("Game Paused");

		if (CheatCodes.Instance != null)
        {
			CheatCodes.Instance.Disable();
		}
	}

	void ResumeGame()
	{
		Time.timeScale = 1;
		m_isGamePaused = false;
		Debug.Log("Game Resumed");

		if (CheatCodes.Instance != null)
		{
			CheatCodes.Instance.Enable();
		}
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

