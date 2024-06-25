using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigHomeState : PigState
{
	private float followDistance = 0.5f;
	private float decelerationRate = 5000.0f;
	public PigHomeState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}

	public override void EnterState()
	{
		//Debug.Log("Enter Home State");
		Pig.walk.Play();
		Pig.animator.Play("Run");
	}

	public override void ExitState()
	{
		//Debug.Log("Exit Home State");
		Pig.walk.Stop();
	}

	public override void UpdateState()
	{
		if (Pig.House == null || Pig.isDropped == false)
		{
			return;
		}
		if (Pig.canHelp == true)
		{
			StateMachine.ChangeState(PigStateMachine.EPigState.Idle);
		}

		Vector3 direction = (Pig.House.position - Pig.transform.position).normalized;
		if (direction.x < 0)
		{
			Pig.sprite.flipX = true;
		}
		else if (direction.x > 0)
		{
			Pig.sprite.flipX = false;
		}

		Pig.rb.velocity = direction * Pig.runSpeed;

		if (Vector3.Distance(Pig.transform.position, Pig.House.position) <= followDistance)
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
