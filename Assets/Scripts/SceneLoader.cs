using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	//Parameters
	[SerializeField] float loadDelay;
	[SerializeField] float sloMoFactor;

	private void Start()
	{
		//the Find is added bc using FindObjectsOfType doesn't find objects under Don't Destroy On Load
		GameObject canvas = GameObject.Find("UI Canvas");	
		canvas.GetComponent<UICanvas>().LinkCamToCanvas();
	}

	public void LoadLevel ()
	{
		if (GetCurrentSceneName() == "GameOver")
		{
			StartCoroutine("LoadStartLevel");
		}
		else
		{
			StartCoroutine("LoadNextLevel");
		}
	}

	public void LoadGameOver()
	{
		SceneManager.LoadSceneAsync("GameOver");
	}

	public void ReloadCurrentLevel()
	{
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadSceneAsync(currentSceneIndex);
	}

	public string GetCurrentSceneName()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		return sceneName;
	}

	IEnumerator LoadStartLevel()
	{
		Time.timeScale = sloMoFactor;
		yield return new WaitForSeconds(loadDelay);
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync(0);
	}

	IEnumerator LoadNextLevel()
	{
		Time.timeScale = sloMoFactor;
		yield return new WaitForSeconds(loadDelay);
		Time.timeScale = 1;
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadSceneAsync(currentSceneIndex + 1);
	}
}
