using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlousSpawner : MonoBehaviour
{
    [SerializeField] GameObject glou;
    [SerializeField] Transform spawnTransform;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float xMaxSpawnForce = 50;
    [SerializeField] float ySpawnForce = 180;



    public void SpawnGlous(int glousNumber){
        StartCoroutine(SpawnGlousCoroutine(glousNumber));
    }


    IEnumerator SpawnGlousCoroutine(int glousNumber)
{
    for (int i =0; i < glousNumber ; i++)
    {
        GameObject newGlou = Instantiate(glou,spawnTransform.position,Quaternion.identity);     // Spawn Glou
        AudioManager.Instance.PlayGlouSpawn();                                                  // Glou spawn sound
        float xRandomForce = Random.Range(-xMaxSpawnForce, xMaxSpawnForce);                     // Calculte force in X
        newGlou.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(xRandomForce,-ySpawnForce));   // Add force on the new glou

        yield return new WaitForSeconds(spawnDelay);
    }
}
}
