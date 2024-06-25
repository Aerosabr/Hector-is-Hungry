using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEatState : WolfState
{
	private float eatTimer;
	private float hungerIncreasePerSecond;
	private float hungerIncreaseThisFrame;
	private bool beginEating = false;
	public WolfEatState(WolfStateMachine stateMachine, Wolf wolf, WolfStateMachine.EWolfState wolfState) : base(stateMachine, wolf, wolfState)
	{
		StateMachine = stateMachine;
		Wolf = wolf;
	}


	public override void EnterState()
	{
		//Debug.Log("Enter Eat State");
		Wolf.animator.Play("Bite");
		MusicManager.instance.soundSources[5].Play();

		eatTimer = Wolf.eatTime;

		beginEating = true;
	}

	public override void ExitState()
	{
		//Debug.Log("Exit Eat State");
		MusicManager.instance.soundSources[5].Stop();
	}

	public override void UpdateState()
	{
		if (beginEating && eatTimer > 0)
		{
			if(Wolf.isSlowed)
			{
				Wolf.AddHunger(((Wolf.foodValue / Wolf.eatTime)/2) * Time.deltaTime);

				eatTimer -= (Wolf.eatingSpeed/2) * Time.deltaTime;
			}
			else
			{
				Wolf.AddHunger((Wolf.foodValue / Wolf.eatTime) * Time.deltaTime);

				eatTimer -= Wolf.eatingSpeed * Time.deltaTime;
			}
		}
		else if (eatTimer <= 0)
		{
			if (Wolf.effect == "Stun")
				StateMachine.ChangeState(WolfStateMachine.EWolfState.Stun);
			else if(Wolf.effect == "Slow")
			{
				Wolf.Slowed();
				StateMachine.ChangeState(WolfStateMachine.EWolfState.Idle);
			}
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
		if (other.TryGetComponent(out IConsumable consumable) && !Wolf.foodInRange.Contains(consumable))
		{
			Wolf.foodInRange.Add(consumable);
		}
	}
}
