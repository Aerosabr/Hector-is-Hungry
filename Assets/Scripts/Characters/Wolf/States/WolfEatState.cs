using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEatState : WolfState
{
	private float eatTimer = 0;
	private bool beginEating = false;
	private float hungerIncreasePerSecond;
	private float hungerIncreaseThisFrame;
	public WolfEatState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState wolfState) : base(stateMachine, wolf, wolfState)
	{
		StateMachine = stateMachine;
		Wolf = wolf;
	}

	public override void EnterState()
	{
		Debug.Log("Enter Eat State");
		eatTimer = Wolf.eatTime;
		hungerIncreasePerSecond = Wolf.foodValue / eatTimer;
		hungerIncreaseThisFrame = hungerIncreasePerSecond * Time.deltaTime;
		beginEating = true;
	}
	public override void ExitState()
	{
		Debug.Log("Exit Eat State");
	}
	public override void UpdateState()
	{
		if (beginEating == true && eatTimer > 0)
		{
			Wolf.currentHunger = Mathf.Lerp(Wolf.currentHunger, Wolf.currentHunger + hungerIncreaseThisFrame, Time.deltaTime * 5f);
			eatTimer -= Wolf.eatingSpeed + Time.deltaTime;
		}
		else if(eatTimer < 0)
		{
			if(Wolf.effect == "Poison")
				StateMachine.ChangeState(WolfStateMachine.EWolfState.Stun);
			else
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
