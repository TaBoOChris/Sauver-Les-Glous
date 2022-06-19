using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

	[Header("Drum")]
	[SerializeField] private GameObject _platformDrumParent;
	private List<GameObject> m_plaftormInDrum = new List<GameObject>();

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
			Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scene");
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

	public GameObject GetPlatformDrumParent()
    {
		return _platformDrumParent;

	}
}
