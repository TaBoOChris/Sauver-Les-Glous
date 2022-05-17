using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Debug.Log("Retour au menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        //GameManager.Instance.StartGame();
        SceneManager.LoadScene("MainLevel");
    }
}
