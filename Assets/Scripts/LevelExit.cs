using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 10)
		{
			FindObjectOfType<SceneLoader>().LoadLevel();
		}
	}
}
