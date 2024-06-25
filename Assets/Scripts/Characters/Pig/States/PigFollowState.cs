using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFollowState : PigState
{
	private float followDistance = 5.0f;
	private float decelerationRate = 5000.0f;
	public PigFollowState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;

	}

	public override void EnterState()
	{
		//Debug.Log("Enter Follow State");
		Pig.walk.Play();
		Pig.animator.Play("Run");
	}

	public override void ExitState()
	{
		//Debug.Log("Exit Follow State");
		Pig.walk.Stop();
	}

	public override void UpdateState()
	{
		if (Pig.Player == null || Pig.isDropped == false)
		{
			return;
		}

		// Calculate direction towards the player
		Vector3 direction = (Pig.Player.position - Pig.transform.position).normalized;
		if (direction.x < 0)
		{
			Pig.sprite.flipX = true;
			Pig.highlightSprite.flipX = true;
		}
		else if (direction.x > 0)
		{
			Pig.sprite.flipX = false;
			Pig.highlightSprite.flipX = false;
		}

		// Move towards the player
		Pig.rb.velocity = direction * Pig.runSpeed;

		// Check distance to stop following
		if (Vector3.Distance(Pig.transform.position, Pig.Player.position) <= followDistance)
		{
			Pig.rb.velocity = Vector3.Lerp(Pig.rb.velocity, Vector3.zero, decelerationRate * Time.deltaTime);

			if (Pig.rb.velocity.magnitude < 0.1f)
			{
				Pig.rb.velocity = Vector3.zero;
				StateMachine.ChangeState(PigStateMachine.EPigState.Idle);
			}
		}
	}

	public override PigStateMachine.EPigState GetState()
	{
		return StateKey;
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
	}

	public override void OnTriggerExit2D(Collider2D other)
	{
	}

	public override void OnTriggerStay2D(Collider2D other)
	{
	}
}
