using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Affichage
    private int minutesUI;
    private int secondsUI;
    private TextMeshProUGUI timerUI;

    // Temps total en secondes
    private int seconds;

    // Coroutine
    Coroutine timerCo;

    private void Awake()
    {
        timerUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void StartTimer(int timeInSeconds)
    {
        if(timerCo != null) StopCoroutine(timerCo);
        seconds = timeInSeconds;
        timerCo = StartCoroutine(Timer_coroutine());
    }

    public void StopTimer()
    {
        StopCoroutine(timerCo);
        timerUI.text = "00 : 00";
    }

    private void DisplayTimer()
    {
        minutesUI = seconds / 60;
        secondsUI = seconds % 60;

        string str = "";
        if (minutesUI < 10) str += "0";
        str += minutesUI.ToString() + " : ";
        if (secondsUI < 10) str += "0";
        str += secondsUI.ToString();

        timerUI.text = str;
    }

    private IEnumerator Timer_coroutine()
    {
        while (seconds >= 0)
        {
            DisplayTimer();
            seconds -= 1;
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.EndGame();
    }


} // class
