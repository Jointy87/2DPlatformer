using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
	//Parameters
	[SerializeField] float scrollSpeed;
	
	void Update()
	{
		ScrollUpwards();
	}

	private void ScrollUpwards()
	{
		transform.Translate(Vector2.up * Time.deltaTime * scrollSpeed);
	}
}
