using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	//Parameters
	[SerializeField] float moveSpeed;

	///Cashes
	Rigidbody2D rb;	

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Move();
	}

	private void Move()
	{
			rb.velocity = new Vector2(moveSpeed, 0);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		moveSpeed = -moveSpeed;
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}
}
