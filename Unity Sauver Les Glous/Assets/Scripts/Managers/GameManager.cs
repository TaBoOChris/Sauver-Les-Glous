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
	private int m_nbGlousSaved;
	private List<GlouInGame> m_glousInGame = new List<GlouInGame>();

	[Header("End menu")]
	[SerializeField] private GameObject m_endMenu;
	[SerializeField] private GameObject m_endMenuVillageBtn;
	[SerializeField] private TextMeshProUGUI m_endMenuText;

	[Header("Pause")]
	[SerializeField] private GameObject m_pauseMenu;
	private static bool m_isGamePaused = false;
	private InputActions m_inputActions;


	// ================================================================

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
        if (AudioManager.Instance)
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


		// Setup GlousStartingList
		List<Glou> glousStartingList;

		if (GlousData.Instance)
        {
			glousStartingList = GlousData.Instance.GetGlousInSelector();
        }
        else
        {
			Debug.Log("GAME MANAGER : NO INSTANCE OF GLOUSDATA");
			glousStartingList = new List<Glou> { 
				new Glou( Glou.SkinGlou.Bleu, 0.8f), 
				new Glou( Glou.SkinGlou.Rouge, 1f), 
				new Glou( Glou.SkinGlou.Vert, 1.2f) };

        }

		Debug.Log("GAME MANAGER : On va spawn " + glousStartingList.Count + " Glous");
		m_nbGlousAlive = glousStartingList.Count;
		m_glousSpawner.SpawnGlous(glousStartingList);

		//SpawnPlaterform();
		//Rotation();
    }

	public void EndGame()
    {
		if (CursorManager.Instance != null)
			CursorManager.Instance.SetPointer();

		if (m_nbGlousAlive <= 0 && m_nbGlousSaved ==0)
        {
			m_endMenuText.text = "Tu n'as pas réussi à sauver les Glous ...";
        }
        else
        {
			m_endMenuText.text = "La machine est enfin arretée !\nTu as sauvé <color=#86E989>" + m_nbGlousSaved + "</color> Glous.  Bien joué !";
        }

		// create list of alive glous
		List<Glou> aliveGlous = GetAliveGlous();
		// Create baby glous
		//List<Glou> babyGlous = GetComponent<BabyGlousCreator>().CreateChildrenGlous(aliveGlous, m_nbGlousAlive);

		EndMenu endMenu = m_endMenu.GetComponent<EndMenu>();
		// display glous killed and saved and babies
		endMenu.DisplayGlous(m_glousInGame);
		//endMenu.DisplayGlous(babyGlous);

		// Create Glous list to give to the Village
		//aliveGlous.AddRange(babyGlous);
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

	// ============= GLOU =====================


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

	public void AddGlou() { m_nbGlousAlive++; }

	public void AddGlouInGame(GlouInGame glouInGame) { m_glousInGame.Add(glouInGame); }

	public void GlouDie()
    {
		m_nbGlousAlive--;
		DebugGlouNumber();
		CheckGlouAlive();
    }

	public int GetNbGlousAlive() { return m_nbGlousAlive; }

	public void AddGlouSaved()
    {
		m_nbGlousSaved++;
		m_nbGlousAlive--;
		DebugGlouNumber();
		CheckGlouAlive();
    }


	private void CheckGlouAlive()
    {
		if (m_nbGlousAlive <= 0)
		{
			Invoke("EndGame", 2f);
		}
	}

	public void DebugGlouNumber()
    {
		Debug.Log("GAME MANAGER : Glou in Drum = " + m_nbGlousAlive + " Glou saved = " + m_nbGlousSaved);

	}


	// ============= PAUSE =============== 

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

	public bool IsGamePaused()	{ return m_isGamePaused; }

	private void OnEnable()		{ m_inputActions.Game.Enable(); }

	private void OnDisable()	{ m_inputActions.Game.Disable(); }
}

