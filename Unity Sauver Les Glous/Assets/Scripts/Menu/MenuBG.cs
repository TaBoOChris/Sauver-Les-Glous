using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBG : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] GameObject glou;
    [SerializeField] float spawnInterval = 2.0f;
    [SerializeField] float xSpawnForce = 180;
    [SerializeField] float ySpawnForce = 180;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        t = spawnInterval - 0.5f;
    }

    void SpawnGlou()
    {
        GameObject newGlou = Instantiate(glou, spawner.position, Quaternion.identity);     // Spawn Glou
        //AudioManager.Instance.PlayGlouSpawn();                                          // Glou spawn sound
        newGlou.transform.parent = this.transform;
        newGlou.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(xSpawnForce+Random.Range(-50f,50f), -ySpawnForce)); // Add force on the new glou

    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
        if(t > spawnInterval)
        {
            SpawnGlou();
            t = 0;
        }
    }
}
