using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	//Parameters
	[SerializeField] int pointValue;
	[SerializeField] AudioClip coinPickupSFX;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other == FindObjectOfType<Player>().FetchBodyCollider())
		{
			AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
			FindObjectOfType<ScoreCounter>().AddScore(pointValue);
			Destroy(gameObject);
		}
	}
}
