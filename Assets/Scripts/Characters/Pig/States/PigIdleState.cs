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
	}
	public override void ExitState()
	{
		Debug.Log("Exit Idle State");
	}
	public override void UpdateState()
	{
		if (Pig.item != null)
			StateMachine.ChangeState(PigStateMachine.EPigState.Transport);
	}
	public override PigStateMachine.EPigState GetState()
	{
		return StateKey;
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		throw new System.NotImplementedException();
	}
	public override void OnTriggerExit2D(Collider2D other)
	{
		throw new System.NotImplementedException();
	}
	public override void OnTriggerStay2D(Collider2D other)
	{
		throw new System.NotImplementedException();
	}
}