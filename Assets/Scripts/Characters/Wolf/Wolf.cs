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
    public float eatingSpeed = 1;
    public float stunTimer = 0;
	public float visionRadius = 10f;

	public float eatTime, foodValue, effectValue;
	public string effect;

	public Collider visionCollider;
	public List<IConsumable> foodInRange = new List<IConsumable>();

	public void AddHunger(float foodValue)
	{
		currentHunger += foodValue;
	}

	public void UpdateSpeed(float speed)
	{
		hungerDrainSpeed = speed;
	}


}
