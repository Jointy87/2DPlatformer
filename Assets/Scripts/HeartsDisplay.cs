using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartsDisplay : MonoBehaviour
{
	//Parameters
	[SerializeField] GameObject heartPrefab;

	//Cashe
	int heartsLeft;
	
	
	void Start()
	{
		int hearts = FindObjectOfType<HeartsCounter>().FetchHearts();
		int[] heartContainers = new int[hearts];

		foreach (int heart in heartContainers)
		{
			int amountSpawned = transform.childCount;

			GameObject heartSpawned = Instantiate(heartPrefab,
				new Vector2(transform.position.x + amountSpawned, transform.position.y),
				Quaternion.identity);

			heartSpawned.transform.parent = transform;
		}
	} 

	public void SubstractHeart()
	{
		if (transform.childCount <= 0) { return; }

		Destroy(GetComponent<Transform>().GetChild(heartsLeft).gameObject);
	}



	
}
