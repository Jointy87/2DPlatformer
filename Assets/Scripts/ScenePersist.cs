using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
	//Cache
	int startingSceneIndex;

	private void Awake()
	{
		SetUpSingleton();
	}
	private void Start()
	{
		startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	private void Update()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		if(currentSceneIndex != startingSceneIndex)
		{
			Destroy(gameObject);
		}
	}

	private void SetUpSingleton()
	{
		{
			if (FindObjectsOfType(GetType()).Length > 1)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}
