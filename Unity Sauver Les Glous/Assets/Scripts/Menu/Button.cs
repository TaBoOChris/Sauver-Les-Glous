using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject m_nextTutoButton;
    [SerializeField] private GameObject m_previousTutoButton;
    [SerializeField] private List<GameObject> m_tutorialList = new List<GameObject>();
    private int m_indexTutoList = 0;

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

    public void DisplayOn(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DisplayOff(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void TutorialDisplayNext()
    {
        m_tutorialList[m_indexTutoList].SetActive(false);
        m_indexTutoList++;
        m_tutorialList[m_indexTutoList].SetActive(true);
        if (m_indexTutoList == m_tutorialList.Count-1)
        {
            m_nextTutoButton.SetActive(false);
        }
        else
        {
            m_tutorialList[m_indexTutoList].SetActive(true);
        }
        if (m_indexTutoList > 0)
        {
            m_previousTutoButton.SetActive(true);
        }
    }

    public void TutorialDisplayPrevious()
    {
        m_tutorialList[m_indexTutoList].SetActive(false);
        m_indexTutoList--;
        m_tutorialList[m_indexTutoList].SetActive(true);
        if (m_indexTutoList == 0)
        {
            m_previousTutoButton.SetActive(false);
        }
        if (m_indexTutoList == m_tutorialList.Count-2)
        {
            m_nextTutoButton.SetActive(true);
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
