using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStunState : WolfState
{
	private float stunTimer = 0;
	public WolfStunState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState wolfState) : base(stateMachine, wolf, wolfState)
	{
		StateMachine = stateMachine;
		Wolf = wolf;
	}

	public override void EnterState()
	{
		stunTimer = Wolf.effectValue;
		Debug.Log("Enter Stun State");
	}
	public override void ExitState()
	{
		Debug.Log("Exit Stun State");
	}
	public override void UpdateState()
	{
		if(stunTimer > 0)
		{
			stunTimer -= 0.9f + Time.deltaTime;
		}
		else
		{
			StateMachine.ChangeState(WolfStateMachine.EWolfState.Idle);
		}
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
