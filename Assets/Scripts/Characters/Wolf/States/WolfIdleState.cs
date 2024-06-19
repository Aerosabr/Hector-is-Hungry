using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdleState : WolfState
{
	public WolfIdleState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState wolfState) : base(stateMachine, wolf, wolfState)
    {
		StateMachine = stateMachine;
        Wolf = wolf;
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
		DrainHunger();
	}
	public override WolfStateMachine.EWolfState GetState()
	{
		return StateKey;
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided");
		// Check if the collided object has a component that implements IConsumable
		//IConsumable consumable = other.GetComponent<IConsumable>();
		//Debug.Log(consumable);
		//Wolf.foodInRange.Add(consumable);
		if (other.TryGetComponent(out IConsumable consumable) && !Wolf.foodInRange.Contains(consumable))
		{
			Wolf.foodInRange.Add(consumable);
		}
	}

	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.TryGetComponent(out IConsumable consumable) && Wolf.foodInRange.Contains(consumable))
		{
			Wolf.foodInRange.Remove(consumable);
		}
	}
	public override void OnTriggerStay2D(Collider2D other)
	{

		if (other.TryGetComponent(out IConsumable consumable) && Wolf.foodInRange.Contains(consumable))
		{
			ConsumeNearbyFood();
		}
	}

	public void DrainHunger()
	{
		if (Wolf.currentHunger > 0)
		{
			Wolf.currentHunger -= Wolf.hungerDrainSpeed / 1000 + Time.deltaTime;
		}
		if(Wolf.currentHunger < 0)
		{
			StateMachine.ChangeState(WolfStateMachine.EWolfState.Destroy);
		}
	}
	private void ConsumeNearbyFood()
	{
		if(Wolf.foodInRange.Count > 0)
		{
			Wolf.foodInRange[0].Consume(out Wolf.eatTime, out Wolf.foodValue, out Wolf.effect, out Wolf.effectValue);
			StateMachine.ChangeState(WolfStateMachine.EWolfState.Eat);
			Debug.Log($"Consumed food with values: eatTime={Wolf.eatTime}, foodValue={Wolf.foodValue}, effect={Wolf.effect}, effectValue={Wolf.effectValue}");
		}
	}
}