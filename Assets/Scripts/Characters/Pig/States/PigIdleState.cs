using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIdleState : PigState
{
	public PigIdleState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}

	public override void EnterState()
	{
		Debug.Log("Enter Idle State");
		Pig.animator.Play("Idle");
	}
	public override void ExitState()
	{
		Debug.Log("Exit Idle State");
	}
	public override void UpdateState()
	{
		if(Pig.isDropped == false)
			return;
		else if (Pig.canHelp == false)
		{
			
			if (Pig.House != null && Vector3.Distance(Pig.transform.position, Pig.House.position) > 1f)
				StateMachine.ChangeState(PigStateMachine.EPigState.Home);
		}
		else if (Pig.item != null)
			StateMachine.ChangeState(PigStateMachine.EPigState.Transport);
		else if (Pig.canHelp == true && Vector3.Distance(Pig.transform.position, Pig.Player.position) > 5.0f)
			StateMachine.ChangeState(PigStateMachine.EPigState.Follow);
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
