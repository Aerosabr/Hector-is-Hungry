using System;
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
		//Debug.Log("Enter Destroy State");
		MusicManager.instance.TransitionDanger(true);
		MusicManager.instance.soundSources[7].Play();
		Wolf.PlayWalkSound();
	}
	public override void ExitState()
	{
		//Debug.Log("Exit Destroy State");
		MusicManager.instance.TransitionDanger(false);
		Wolf.StopWalkSound();
	}
	public override void UpdateState()
	{
		Vector3 targetPosition = Wolf.transform.position + Vector3.right * 5f; 

		float lerpSpeed = 0.25f; 
		Wolf.transform.position = Vector3.Lerp(Wolf.transform.position, targetPosition, lerpSpeed * Time.deltaTime);
	}
	public override WolfStateMachine.EWolfState GetState()
	{
		return StateKey;
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		Item item = other.GetComponent<Item>(); // Attempt to get the Item component
		Debug.Log(other);
		if (item != null)
		{
			if (item.isMarked)
			{
				// Perform actions if the item is marked
				Debug.Log("Item is marked");
				// Example: Call a function to process the marked item
				if (other.TryGetComponent(out IConsumable consumable) && !Wolf.foodInRange.Contains(consumable))
				{
					Wolf.foodInRange.Add(consumable);
				}
				Wolf.foodInRange[0].Consume(out Wolf.eatTime, out Wolf.foodValue, out Wolf.effect, out Wolf.effectValue, Wolf.transform);
				Wolf.foodInRange.RemoveAt(0);
				//Wolf.foodInRange.RemoveAt(0);
				StateMachine.ChangeState(WolfStateMachine.EWolfState.Eat);
			}
			else
			{
				// Destroy the GameObject associated with the collider
				if (other.TryGetComponent(out DestroyOnContact contact))
				{
					contact.DestroyObject();
				}
				Wolf.animator.Play("DBite");
				Wolf.PlayDeleteSound();

			}
		}
		else
		{
			if (other.TryGetComponent(out DestroyOnContact contact))
			{
				// Check if the collision is with a player
				if (other.gameObject.CompareTag("Player"))
				{
					// Check if the player has a BoxCollider2D
					if (other.GetType().ToString() == "UnityEngine.BoxCollider2D")
					{
						contact.DestroyObject();
					}
				}
				else
				{
					contact.DestroyObject();
				}
				Wolf.animator.Play("DBite");
				Wolf.PlayDeleteSound();
				
			}
		}
	}

	public override void OnTriggerExit2D(Collider2D other)
	{

	}

	public override void OnTriggerStay2D(Collider2D other)
	{
		Item item = other.GetComponent<Item>(); // Attempt to get the Item component
		Debug.Log(other);
		if (item != null)
		{
			if (item.isMarked)
			{
				// Perform actions if the item is marked
				Debug.Log("Item is marked");
				// Example: Call a function to process the marked item
				if (other.TryGetComponent(out IConsumable consumable) && !Wolf.foodInRange.Contains(consumable))
				{
					Wolf.foodInRange.Add(consumable);
				}
				Wolf.foodInRange[0].Consume(out Wolf.eatTime, out Wolf.foodValue, out Wolf.effect, out Wolf.effectValue, Wolf.transform);
				Wolf.foodInRange.RemoveAt(0);
				//Wolf.foodInRange.RemoveAt(0);
				StateMachine.ChangeState(WolfStateMachine.EWolfState.Eat);
			}
			else
			{
				// Destroy the GameObject associated with the collider
				if (other.TryGetComponent(out DestroyOnContact contact))
				{
					contact.DestroyObject();
				}
				Wolf.animator.Play("DBite");
				Wolf.PlayDeleteSound();

			}
		}
		else
		{
			if (other.TryGetComponent(out DestroyOnContact contact))
			{
				// Check if the collision is with a player
				if (other.gameObject.CompareTag("Player"))
				{
					// Check if the player has a BoxCollider2D
					if (other.GetType().ToString() == "UnityEngine.BoxCollider2D")
					{
						contact.DestroyObject();
					}
				}
				else
				{
					contact.DestroyObject();
				}
				Wolf.animator.Play("DBite");
				Wolf.PlayDeleteSound();

			}
		}
	}
}
