using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _nextTutoButton;
    [SerializeField] private GameObject _previousTutoButton;
    [SerializeField] private List<GameObject> _tutorialList = new List<GameObject>();
    private int _indexTutoList = 0;

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

    public void TutorialDisplayNext()
    {
        _tutorialList[_indexTutoList].SetActive(false);
        _indexTutoList++;
        _tutorialList[_indexTutoList].SetActive(true);
        if (_indexTutoList == _tutorialList.Count-1)
        {
            _nextTutoButton.SetActive(false);
        }
        else
        {
            _tutorialList[_indexTutoList].SetActive(true);
        }
        if (_indexTutoList > 0)
        {
            _previousTutoButton.SetActive(true);
        }
    }

    public void TutorialDisplayPrevious()
    {
        _tutorialList[_indexTutoList].SetActive(false);
        _indexTutoList--;
        _tutorialList[_indexTutoList].SetActive(true);
        if (_indexTutoList == 0)
        {
            _previousTutoButton.SetActive(false);
        }
        if (_indexTutoList == _tutorialList.Count-2)
        {
            _nextTutoButton.SetActive(true);
        }
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
