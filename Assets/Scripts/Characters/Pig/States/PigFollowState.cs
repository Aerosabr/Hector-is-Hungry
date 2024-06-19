using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFollowState : PigState
{
	public PigFollowState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}

	public override void EnterState()
	{
		Debug.Log("Enter Follow State");
		Pig.destinationSetter.target = Pig.Player;
	}
	public override void ExitState()
	{
		Debug.Log("Exit Follow State");
	}
	public override void UpdateState()
	{
		
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
