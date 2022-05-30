using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_glou;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private Transform m_glousParentGO;
    [SerializeField] private float m_spawnDelay = 1f;
    [SerializeField] private float m_xMaxSpawnForce = 50;
    [SerializeField] private float m_ySpawnForce = 180;
    [SerializeField] private float m_scaleMax = 1.2f;
    [SerializeField] private float m_scaleMin = 0.6f;

    [SerializeField] private Color m_defaultGlousColor = new Color(182, 122, 216);

    public void SpawnGlous(int glousNumber){
        StartCoroutine(SpawnGlousCoroutine(glousNumber));
    }

    IEnumerator SpawnGlousCoroutine(int glousNumber)
    {
        for (int i=0; i < glousNumber; i++)
        {
            GameObject newGlou = Instantiate(m_glou, m_spawnTransform.position, Quaternion.identity, m_glousParentGO); // Spawn Glou

            newGlou.GetComponentInChildren<SpriteRenderer>().color = m_defaultGlousColor;
            SetUpNewGlou(newGlou);

            yield return new WaitForSeconds(m_spawnDelay);
        }
    }

    public void SpawnGlous(List<Glou> glousList)
    {
        StartCoroutine(SpawnGlousCoroutine(glousList));
    }

    IEnumerator SpawnGlousCoroutine(List<Glou> glousList)
    {
        for (int i=0; i < glousList.Count; i++)
        {
            GameObject newGlou = Instantiate(m_glou, m_spawnTransform.position, Quaternion.identity, m_glousParentGO); // Spawn Glou

            newGlou.GetComponentInChildren<SpriteRenderer>().color = Color.HSVToRGB(glousList[i].GetHue(), 1, 1);
            SetUpNewGlou(newGlou);

            yield return new WaitForSeconds(m_spawnDelay);
        }
    }

    public void SetUpNewGlou(GameObject newGlou)
    {
        // Change Size
        float scale = Random.Range(m_scaleMin, m_scaleMax);
        newGlou.transform.localScale = new Vector3(scale, scale, scale);

        AudioManager.Instance.PlayGlouSpawn(); // Glou spawn sound
        GameManager.Instance.AddGlou();

        float xRandomForce = Random.Range(-m_xMaxSpawnForce, m_xMaxSpawnForce); // Calculte force in X
        newGlou.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(xRandomForce, -m_ySpawnForce)); // Add force on the new glou
    }
}
