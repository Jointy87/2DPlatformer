using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsCounter : MonoBehaviour
{
	//Parameters
	[SerializeField] int hearts;
	
	public void SubstractHeart()
	{
		hearts--;
		FindObjectOfType<HeartsDisplay>().SubstractHeart();
		Debug.Log(hearts);
	}
	public int FetchHearts()
	{
		return hearts;
	}
}
