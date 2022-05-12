using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
	[SerializeField] private string m_objectID;

	private void Awake()
	{
		m_objectID = name + transform.position.ToString();
	}

	void Start()
	{
		for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
		{
			if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
			{
				if (Object.FindObjectsOfType<DontDestroy>()[i].m_objectID == m_objectID)
				{
					Destroy(gameObject);
				}
			}

		}
		DontDestroyOnLoad(gameObject);
	}
}
