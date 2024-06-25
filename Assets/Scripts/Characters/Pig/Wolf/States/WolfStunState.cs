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
		Wolf.animator.Play("Idle");
		stunTimer = Wolf.effectValue;
		Wolf.sprite.color = Color.yellow;
		Wolf.transform.GetChild(2).gameObject.SetActive(true);
		//Debug.Log("Enter Stun State");
	}
	public override void ExitState()
	{
		Wolf.sprite.color = Color.white;
		Wolf.transform.GetChild(2).gameObject.SetActive(false);
		//Debug.Log("Exit Stun State");
	}
	public override void UpdateState()
	{
		if(stunTimer > 0)
		{
			stunTimer -= 1 * Time.deltaTime;
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
	}
}
