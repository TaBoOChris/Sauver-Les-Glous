using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
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

    public void ReloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Load Scene " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
    }


}
