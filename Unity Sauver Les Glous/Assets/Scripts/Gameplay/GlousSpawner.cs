using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_glou;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private float m_spawnDelay = 1f;
    [SerializeField] private float m_xMaxSpawnForce = 50;
    [SerializeField] private float m_ySpawnForce = 180;
    [SerializeField] private float m_scaleMax = 1.2f;
    [SerializeField] private float m_scaleMin = 0.6f;

    public void SpawnGlous(int glousNumber){
        StartCoroutine(SpawnGlousCoroutine(glousNumber));
    }

    IEnumerator SpawnGlousCoroutine(int glousNumber)
    {
        for (int i=0; i < glousNumber; i++)
        {
            GameObject newGlou = Instantiate(m_glou, m_spawnTransform.position, Quaternion.identity); // Spawn Glou

            // Change Size
            float scale = Random.Range(m_scaleMin, m_scaleMax);
            newGlou.transform.localScale = new Vector3(scale, scale, scale);
            
            AudioManager.Instance.PlayGlouSpawn(); // Glou spawn sound
            GameManager.Instance.AddGlou();

            float xRandomForce = Random.Range(-m_xMaxSpawnForce, m_xMaxSpawnForce); // Calculte force in X
            newGlou.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(xRandomForce, -m_ySpawnForce)); // Add force on the new glou

            yield return new WaitForSeconds(m_spawnDelay);
        }
    }
}
