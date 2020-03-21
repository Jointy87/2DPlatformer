using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// Parameters
	[Header("Values")]
	[SerializeField] int playerHP;
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpForce;
	[SerializeField] float climbSpeed;
	[SerializeField] float climbHorizontalMultiplier;

	[Header("Colliders")]
	[SerializeField] BoxCollider2D bodyCollider;
	[SerializeField] BoxCollider2D feetCollider;

	[Header("Death")]
	[SerializeField] ParticleSystem deathVFX;

	// Cashe
	Rigidbody2D rb;
	SpriteRenderer spriteInChild;
	Animator animator;
	RaycastHit2D onGroundCheck;
	float gravityAtStart;
	bool isPlayerAlive;
	ParticleSystem deathVFXParticles;
	int collisionCounter;



	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteInChild = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponent<Animator>();

		gravityAtStart = rb.gravityScale;
		isPlayerAlive = true;
		collisionCounter = 0;
	}

	void Update()
	{
		if (isPlayerAlive)
		{
			MovePlayer();
			Jump();
			Climbing();
			CheckPlayerDirection();
			Die();
		}

		if (!isPlayerAlive)
		{
			StopMovingUponDeath();
		}

	}
	

	private void MovePlayer()
	{
		rb.velocity =
			new Vector2(Input.GetAxis("Horizontal") * moveSpeed,
			rb.velocity.y);

		bool playerIsRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
		animator.SetBool("IsRunning", playerIsRunning);
	}
	private void CheckPlayerDirection()
	{
		if (Input.GetAxis("Horizontal") > 0)
		{
			spriteInChild.flipX = false;
			bodyCollider.offset = new Vector2(-0.03f, bodyCollider.offset.y);
			feetCollider.offset = new Vector2(-0.03f, feetCollider.offset.y);
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			spriteInChild.flipX = true;
			bodyCollider.offset = new Vector2(0.03f, bodyCollider.offset.y);
			feetCollider.offset = new Vector2(0.03f, feetCollider.offset.y);
		}
	}
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			Vector2 AddedJumpVelocity = new Vector2(0f, jumpForce);
			rb.velocity += AddedJumpVelocity;
		}
	}

	private void Climbing()
	{
		if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
		{
			if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
			{
				ClimbingMovement();
			}

			else if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
				(Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon ||
				Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon))
			{
				ClimbingMovement();
			}

			else if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && Mathf.Abs(Input.GetAxis("Vertical")) < Mathf.Epsilon)
			{
				rb.gravityScale = 0f;

				animator.SetBool("IsClimbing", true);

				StartCoroutine("DisableAnimatorOnTimer", 0.1f);

				rb.velocity =
				new Vector2(Input.GetAxis("Horizontal") * climbHorizontalMultiplier,
				Input.GetAxis("Vertical") * climbSpeed);
			}
		}
		else
		{
			rb.gravityScale = gravityAtStart;

			animator.enabled = true;
			animator.SetBool("IsClimbing", false);
		}
	}

	private void ClimbingMovement()
	{
		rb.gravityScale = 0f;

		animator.enabled = true;
		animator.SetBool("IsClimbing", true);

		rb.velocity =
		new Vector2(Input.GetAxis("Horizontal") * climbHorizontalMultiplier,
		Input.GetAxis("Vertical") * climbSpeed);
	}
	IEnumerator DisableAnimatorOnTimer(float time)
	{
		yield return new WaitForSeconds(time);
		animator.enabled = false;
	}

	private void Die()
	{
		if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
		{
			animator.SetTrigger("IsDead");
			rb.velocity = new Vector2(Random.Range(-5, 5), Random.Range(10, 15));
			isPlayerAlive = false;

			FindObjectOfType<HeartsCounter>().SubstractHeart();

			TriggerDeathVFX();
			feetCollider.sharedMaterial = null;
			bodyCollider.sharedMaterial = null;
			StartCoroutine("DisableFeetCollider");
		}
	}
	IEnumerator DisableFeetCollider()
	{
		feetCollider.enabled = false;
		yield return new WaitForSeconds(.1f);
		feetCollider.enabled = true;
	}

	private void TriggerDeathVFX()
	{
		deathVFXParticles = Instantiate(deathVFX, transform.position, Quaternion.identity);
		Destroy(deathVFXParticles, 5f);
		deathVFXParticles.transform.parent = transform;
	}

	private void StopMovingUponDeath()
	{
		if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			collisionCounter++;

			if(collisionCounter >= 2) { return; }	//Prevents from code beneath to be executed multiple times
													//Which resulted in levels being loaded multiple times

			animator.SetTrigger("IsTotallyDead");

			if (!deathVFXParticles) { return; }
			deathVFXParticles.Stop();

			int heartsLeft = FindObjectOfType<HeartsCounter>().FetchHearts();
			if(heartsLeft == 0)
			{
				StartCoroutine("WaitForGameOver");
			}
			else
			{
				StartCoroutine("WaitForRespawn");
			}
		}
	}

	IEnumerator WaitForGameOver()
	{
		yield return new WaitForSeconds(1);
		FindObjectOfType<SceneLoader>().LoadGameOver();
	}
	IEnumerator WaitForRespawn()
	{
		yield return new WaitForSeconds(1);
		FindObjectOfType<SceneLoader>().ReloadCurrentLevel();
	}

	public BoxCollider2D FetchBodyCollider()
	{
		return bodyCollider;
	}
}
