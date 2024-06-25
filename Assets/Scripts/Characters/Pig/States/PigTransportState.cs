using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTransportState : PigState
{
	private float decelerationRate = 5000.0f;
	public PigTransportState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}
		

	public override void EnterState()
	{
		Debug.Log("Enter Transfer State");
		Pig.walk.Play();
		Pig.animator.Play("Run");
	}

	public override void ExitState()
	{
		Debug.Log("Exit Transfer State");
		Pig.walk.Stop();
	}

	public override void UpdateState()
	{
		if (Pig.Wolf == null)
		{
			Debug.LogError("Wolf transform not assigned to Pig.");
			return;
		}

		// Calculate direction towards the player
		Vector3 direction = (Pig.Wolf.position - Pig.transform.position).normalized;

		// Move towards the player
		Pig.rb.velocity = direction * Pig.runSpeed;
		Pig.animator.speed = Pig.runSpeed;
		if (direction.x < 0)
		{
			Pig.sprite.flipX = true;
			Pig.highlightSprite.flipY = true;
		}
		else if (direction.x > 0)
		{
			Pig.sprite.flipX = false;
			Pig.highlightSprite.flipY = false;
		}

		if (Vector3.Distance(Pig.transform.position, Pig.Wolf.position) <= 0.5f)
		{
			Pig.rb.velocity = Vector3.Lerp(Pig.rb.velocity, Vector3.zero, decelerationRate * Time.deltaTime);

			if (Pig.rb.velocity.magnitude < 0.1f)
			{
				Pig.rb.velocity = Vector3.zero;
				Pig.animator.Play("Idle");
				Pig.item.GetComponent<Item>().ItemDropped(Pig.gameObject);
				Pig.item = null;
				Pig.runSpeed = 2.5f;
				Pig.animator.speed = Pig.runSpeed;
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
