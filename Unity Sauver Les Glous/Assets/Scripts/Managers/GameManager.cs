using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[Header("Glous")]
	[SerializeField] private GlousSpawner m_glousSpawner;
	private int m_nbGlousAlive;
	private List<GlouInGame> m_glousInGame = new List<GlouInGame>();

	[Header("Timer")]
	[SerializeField] private Timer m_timer;
	[SerializeField] private int m_gameTime = 60;

	[Header("End menu")]
	[SerializeField] private GameObject m_endMenu;
	[SerializeField] private GameObject m_endMenuVillageBtn;
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
		AudioManager.Instance.PlayGameMusic();
		StartGame();
		if(CursorManager.Instance != null)
			CursorManager.Instance.SetPointer();
	}

	public void StartGame()
    {
		Time.timeScale = 1;
		m_isGamePaused = false;

		m_pauseMenu.SetActive(false);
		m_endMenu.SetActive(false);

		//List<Glou> glousStartingList = new List<Glou> { new Glou(0.2f, 0.8f), new Glou(0.5f, 1f), new Glou(0.8f, 1.2f) };
		List<Glou> glousStartingList = GlousData.Instance.GetGlousInSelector();

		m_nbGlousAlive = glousStartingList.Count;
		m_glousSpawner.SpawnGlous(glousStartingList);

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

		// create list of alive glous
		List<Glou> aliveGlous = GetAliveGlous();
		// Create baby glous
		List<Glou> babyGlous = GetComponent<BabyGlousCreator>().CreateChildrenGlous(aliveGlous, m_nbGlousAlive);

		EndMenu endMenu = m_endMenu.GetComponent<EndMenu>();
		// display glous killed and saved and babies
		endMenu.DisplayGlous(m_glousInGame);
		endMenu.DisplayGlous(babyGlous);

		// Create Glous list to give to the Village
		aliveGlous.AddRange(babyGlous);
		if(GlousData.Instance)
			GlousData.Instance.SetGlousInSelector(aliveGlous);
		// GlousData.m_glousInSelector = glousAlive + babyGlous

		// Plus utile car maintenant il y a toujours au minimun 4 Glous Rouge/Bleu/Jaune
		/*if(GlousData.Instance.GetGlousInSelector().Count == 0 && GlousData.Instance.GetGlousInVillage().Count == 0)
        {
			m_endMenuVillageBtn.SetActive(false);
        }*/

		// pop up end menu
		m_endMenu.SetActive(true);
		Time.timeScale = 0f;
		m_isGamePaused = true;
		// must not be able to pause/unpause when in endMenu
		m_inputActions.Game.Disable();
	}

	private List<Glou> GetAliveGlous()
    {
		List<Glou> aliveGlous = new List<Glou>();

		foreach (GlouInGame glou in m_glousInGame)
		{
			if (glou.IsAlive())
			{
				aliveGlous.Add(glou.GetGlou());
			}
		}

		return aliveGlous;
	}

	public void AddGlou()
    {
		m_nbGlousAlive++;
    }

	public void AddGlouInGame(GlouInGame glouInGame)
    {
		m_glousInGame.Add(glouInGame);
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

