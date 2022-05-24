using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_endButtons;

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
}
