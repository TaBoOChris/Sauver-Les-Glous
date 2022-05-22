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


	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scene");
			return;
		}

		Instance = this;
	}

	void Start()
    {
		StartGame();
    }

    void Update()
    {
        
    }

	public void StartGame()
    {
		endMenu.SetActive(false);
		_NbGlousAlive = _NbGlousStartLevel;
		glousSpawner.SpawnGlous(_NbGlousStartLevel);	//SpawnGlous();
		//SpawnPlaterform();
		//Rotation();
		timer.StartTimer(gameTime);							//StartTimer();
    }

	public void EndGame()
    {
		if(_NbGlousAlive <= 0)
        {
			endMenuText.text = "Tu n'as pas réussi à sauver les Glous ...";
        }
        else
        {
			endMenuText.text = "La machine est enfin arretée !\nTu as sauvé <color=#86E989>" + _NbGlousAlive + "</color> Glous.  Bien joué !";
        } 
		//StopRotation()
		endMenu.SetActive(true);
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
}
