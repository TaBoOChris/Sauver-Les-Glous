using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private int _NbGlousStartLevel = 10;
	private int _NbGlousAlive;

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
		//SpawnGlous();
		//SpawnPlaterform();
		//Rotation();
		//StartTimer();
    }

	public void EndGame()
    {
		Debug.Log("Fin de la partie");
		if(_NbGlousAlive <= 0)
        {
			Debug.Log("Tu n'as pas réussi à sauver les Glous :'(");
        }
        else
        {
			Debug.Log("La machine est enfin arrêtée!\nTu as sauvé " + _NbGlousAlive + " Glous. Bien joué!");
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
