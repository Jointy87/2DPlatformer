using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
	private void Awake()
	{
		SetUpSingleton();
	}

	private void SetUpSingleton()
	{
		string currentSceneName = FindObjectOfType<SceneLoader>().GetCurrentSceneName();
		Debug.Log(currentSceneName);

		if (currentSceneName == "GameOver")
		{
			var counter = GameObject.FindGameObjectsWithTag("GameSession");
			foreach (var item in counter)
			{
				Destroy(item);
			}
		}
		else
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
