using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_glou;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private GameObject m_glousParentGO;
    [SerializeField] private float m_spawnDelay = 1f;
    [SerializeField] private float m_xMaxSpawnForce = 50;
    [SerializeField] private float m_ySpawnForce = 180;
    [SerializeField] private float m_scaleMax = 1.2f;
    [SerializeField] private float m_scaleMin = 0.6f;

    [SerializeField] private Color m_defaultGlousColor = new Color(229, 118, 238); // pink

    public void SpawnGlous(List<Glou> glousList)
    {
        StartCoroutine(SpawnGlousCoroutine(glousList));
    }

    IEnumerator SpawnGlousCoroutine(List<Glou> glousList)
    {
        for (int i = 0; i < glousList.Count; i++)
        {
            SpawnGlou(glousList[i]);

            yield return new WaitForSeconds(m_spawnDelay);
        }
    }

    public void SpawnGlou(Glou glou)
    {
        Color color;
        float scale;

        GameObject newGlou = Instantiate(m_glou, m_spawnTransform.position, Quaternion.identity, m_glousParentGO.transform);
        newGlou.GetComponent<GlouInGame>().SetGlou(glou);

        if (glou != null)
        {
            color = Color.HSVToRGB(glou.hue, 1, 1);
            scale = glou.sizeMultiplier;

            GameManager.Instance.AddGlouInGame(newGlou.GetComponent<GlouInGame>());
        }
        else
        {
            color = m_defaultGlousColor;
            scale = 1f;
        }

        // set glouGO color
        newGlou.GetComponentInChildren<SpriteRenderer>().color = color;
        // set glouGO size
        newGlou.transform.localScale = new Vector3(scale, scale, scale);

        AudioManager.Instance.PlayGlouSpawn();

        float xRandomForce = Random.Range(-m_xMaxSpawnForce, m_xMaxSpawnForce); // Calculte force in X
        newGlou.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(xRandomForce, -m_ySpawnForce)); // Add force on the new glou
    }

    public void SpawnGlou()
    {
        SpawnGlou(null);
    }

    public void SpawnNewGlou()
    {
        float hue = Random.Range(0f, 1f);
        float scale = Random.Range(m_scaleMin, m_scaleMax);

        Glou glou = new Glou(hue, scale);

        SpawnGlou(glou);
    }

    public GameObject GetGlousParentGO()
    {
        return m_glousParentGO;
    }
}
