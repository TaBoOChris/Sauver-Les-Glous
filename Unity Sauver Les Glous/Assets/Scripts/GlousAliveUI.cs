using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlousAliveUI : MonoBehaviour
{
    private TextMeshProUGUI glousAlive;

    // Start is called before the first frame update
    void Start()
    {
        glousAlive = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        glousAlive.text = GameManager.Instance.GetNbGlousAlive().ToString();
    }
}
