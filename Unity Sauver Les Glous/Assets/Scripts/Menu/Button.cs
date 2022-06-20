using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load Scene " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void PressAnimation()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Load Scene " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
    }

    public void TutorialDisplayOn()
    {
        _tutorial.SetActive(true);
    }

    public void TutorialDisplayOff()
    {
        _tutorial.SetActive(false);
    }

    public void ReloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Load Scene " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
    }

    public void ResumeGame()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.ResumeGame();
        }
    }

}
