using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

	[Header("Drum")]
	[SerializeField] private GameObject _platformDrumParent;
	[SerializeField] private List<GameObject> m_plaftormInDrum = new List<GameObject>();

	[Header("Stock")]
	[SerializeField] private GameObject _platformStockParent;
	[SerializeField] private List<GameObject> m_stockAnchor = new List<GameObject>();
	private List<GameObject> m_plaftormInStock = new List<GameObject>();

	[Header("Platform prefab")]
	[SerializeField] private List<GameObject> m_plaftormPrefab = new List<GameObject>();

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("Il y a plus d'une instance de PlatformManager dans la scene");
			return;
		}

		Instance = this;
	}

    private void Start()
    {
		PlatformSpawn();
	}

	private void PlatformSpawn()
    {
		for(int i =0; i<m_stockAnchor.Count; i++)
        {
			int randomNumber = Random.Range(0, m_plaftormPrefab.Count);
			GameObject newPlatform = Instantiate(m_plaftormPrefab[randomNumber], m_stockAnchor[i].transform.position, Quaternion.identity, m_stockAnchor[i].transform);
			newPlatform.GetComponent<Platform>().enabled = false;
			newPlatform.GetComponent<PlatformStock>().enabled = true;
			newPlatform.transform.localScale = Vector3.one * Random.Range(0.5f, 1.0f);
			newPlatform.transform.eulerAngles = new Vector3(newPlatform.transform.eulerAngles.x, newPlatform.transform.eulerAngles.y, newPlatform.transform.eulerAngles.z + Random.Range(0f, 180.0f));
		}
    }

	public void PlatformSpawnSpecificAnchor(Transform anchor)
    {
		int randomNumber = Random.Range(0, m_plaftormPrefab.Count);
		GameObject newPlatform = Instantiate(m_plaftormPrefab[randomNumber], anchor.position, Quaternion.identity, anchor.transform);
		newPlatform.GetComponent<Platform>().enabled = false;
		newPlatform.GetComponent<PlatformStock>().enabled = true;
		newPlatform.transform.localScale = Vector3.zero;
		StartCoroutine(ScaleRotateSpawn(newPlatform));
		//newPlatform.transform.eulerAngles = new Vector3(newPlatform.transform.eulerAngles.x, newPlatform.transform.eulerAngles.y, newPlatform.transform.eulerAngles.z + Random.Range(0f, 180.0f));
	}

	IEnumerator ScaleRotateSpawn(GameObject platform)
	{
		float startTime = Time.time;
		Vector3 newRotation = new Vector3(platform.transform.eulerAngles.x, platform.transform.eulerAngles.y, platform.transform.eulerAngles.z + Random.Range(0f, 180.0f));

		while (Time.time - startTime <= 1)
		{
			platform.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * Random.Range(0.5f, 1.0f), Time.time - startTime);
			platform.transform.eulerAngles = Vector3.Lerp(platform.transform.eulerAngles, newRotation, Time.time - startTime);
			yield return 1;
		}
	}

	public void AddPlateformDrum(GameObject platform)
    {
		m_plaftormInDrum.Add(platform);
	}

	public GameObject GetPlatformDrumParent()
    {
		return _platformDrumParent;

	}
}
