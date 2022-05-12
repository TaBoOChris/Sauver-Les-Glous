using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private int _NbGlousStartLevel = 10;
	private int _NbGlousAlive;

	[SerializeField] GlousSpawner glousSpawner;
	[SerializeField] Timer timer;
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
		_NbGlousAlive = _NbGlousStartLevel;
		glousSpawner.SpawnGlous(_NbGlousStartLevel);	//SpawnGlous();
		//SpawnPlaterform();
		//Rotation();
		timer.StartTimer(60);							//StartTimer();
    }

	public void EndGame()
    {
		Debug.Log("Fin de la partie");
		if(_NbGlousAlive <= 0)
        {
			Debug.Log("Tu n'as pas reussi a sauver les Glous :'(");
        }
        else
        {
			Debug.Log("La machine est enfin arretee!\nTu as sauve " + _NbGlousAlive + " Glous. Bien joue!");
        }
		//StopRotation()
		//EndGameScreen();
	}

	public void GlouDie()
    {
		_NbGlousAlive--;
		if(_NbGlousAlive <= 0)
        {
			EndGame();
        }
    }
}
