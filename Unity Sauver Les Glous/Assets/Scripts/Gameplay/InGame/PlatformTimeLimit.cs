using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTimeLimit : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 10.0f;
    private Color baseColor;
    private bool isFlash = false;

    void Start()
    {
        StartCoroutine(PlatformLimitTime_Coroutine());
        baseColor = gameObject.GetComponentInChildren<Renderer>().material.color;
    }

    private void Update()
    {
        Flash();
    }


    IEnumerator PlatformLimitTime_Coroutine()
    {
        yield return new WaitForSecondsRealtime(_timeLimit - 3.0f);
        isFlash = true;
        yield return new WaitForSecondsRealtime(3.0f);
        Destroy(gameObject);
    }

    private void Flash()
    {
        if (isFlash)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, 0.1f), Mathf.PingPong(Time.time, 0.5f));
        }
    }
}
