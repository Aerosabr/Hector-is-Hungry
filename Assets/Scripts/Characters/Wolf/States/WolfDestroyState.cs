using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDestroyState : WolfState
{
	public WolfDestroyState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState wolfState) : base(stateMachine, wolf, wolfState)
	{
		StateMachine = stateMachine;
		Wolf = wolf;
	}

	public override void EnterState()
	{
		Debug.Log("Enter Destroy State");
	}
	public override void ExitState()
	{
		Debug.Log("Exit Destroy State");
	}
	public override void UpdateState()
	{
		throw new System.NotImplementedException();
	}
	public override WolfStateMachine.EWolfState GetState()
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
