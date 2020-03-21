using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
	public void LinkCamToCanvas()
	{
		Canvas canvas = GetComponent<Canvas>();
		canvas.worldCamera = Camera.main;
	}
}
