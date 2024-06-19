using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTransportState : PigState
{
	public PigTransportState(PigStateMachine stateMachine, Pig pig, PigStateMachine.EPigState stateKey) : base(stateMachine, pig, stateKey)
	{
		StateMachine = stateMachine;
		Pig = pig;
	}

	public override void EnterState()
	{
		Debug.Log("Enter Transport State");
		Pig.destinationSetter.target = Pig.Wolf;
	}
	public override void ExitState()
	{
		Debug.Log("Exit Transport State");
	}
	public override void UpdateState()
	{
		if(Vector3.Distance(Pig.transform.position, Pig.Wolf.transform.position) < 0.1f)
		{
			//Drop Item
			Pig.item = null;
			StateMachine.ChangeState(PigStateMachine.EPigState.Follow);
		}
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
