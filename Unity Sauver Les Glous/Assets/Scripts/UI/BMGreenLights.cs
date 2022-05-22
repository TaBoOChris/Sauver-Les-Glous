using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMGreenLights : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private int index = 0;

    [SerializeField] private float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(GreenLights_coroutine(delay));
    }

    IEnumerator GreenLights_coroutine(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            sr.sprite = sprites[index % sprites.Length];
            index++;
        }
        
    }
}
