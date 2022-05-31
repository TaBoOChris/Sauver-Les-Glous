using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_endButtons;
    [SerializeField] private GameObject m_gridSurvivorGlous;

    [SerializeField] private GameObject m_glouUI;
    [SerializeField] private GameObject m_glouDeadUI;

    private void OnEnable()
    {
        StartCoroutine(EnableEndMenuCoroutine());
    }

    IEnumerator EnableEndMenuCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        m_endButtons.SetActive(true);
    }

    private void OnDisable()
    {
        m_endButtons.SetActive(false);
    }

    public void DisplayGlous(List<GlouInGame> glous)
    {
        foreach (GlouInGame glou in glous)
        {
            AddGlouToGrid(glou.GetGlou().hue, glou.IsAlive());
        }
    }

    public void DisplayGlous(List<Glou> glous)
    {
        foreach (Glou glou in glous)
        {
            AddGlouToGrid(glou.hue, true);
        }
    }

    private void AddGlouToGrid(float hue, bool isAlive)
    {
        GameObject newGlou;
        if (isAlive)
        {
            newGlou = Instantiate(m_glouUI, m_gridSurvivorGlous.transform);
        }
        else
        {
            newGlou = Instantiate(m_glouDeadUI, m_gridSurvivorGlous.transform);
        }
        newGlou.GetComponentInChildren<Image>().color = Color.HSVToRGB(hue, 1, 1);
    }
}
