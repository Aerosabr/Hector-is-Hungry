using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wolf : MonoBehaviour
{
	public float bodySize = 1;
	public float maxHunger = 100;
	public float currentHunger = 100;
	public float hungerDrainSpeed = 1;
	public float eatingSpeed = 1f;
	public float stunTimer = 0;
	public float visionRadius = 10f;

	public float eatTime, foodValue, effectValue;
	public string effect;

	public Collider visionCollider;
	public List<IConsumable> foodInRange = new List<IConsumable>();

	private float timeSinceLastIncrease = 0f;

	private void Update()
	{
		timeSinceLastIncrease += Time.deltaTime;

		if (timeSinceLastIncrease >= 60f)
		{
			IncreaseEatingSpeed();
			timeSinceLastIncrease = 0f;
		}

	}

	private void IncreaseEatingSpeed()
	{
		eatingSpeed += 0.1f;
	}

	public void AddHunger(float foodValue)
	{
		if(currentHunger < maxHunger)
		{
			currentHunger += foodValue;
		}
	}

	public void UpdateSpeed(float speed)
	{
		hungerDrainSpeed = speed;
	}
}
