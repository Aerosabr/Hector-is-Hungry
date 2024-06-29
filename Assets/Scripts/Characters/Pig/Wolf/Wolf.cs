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

	public bool GracePeriod = false;
	public SpriteRenderer sprite;
	public float slowTimer = 0;
	public float slowDuration;
	public bool isSlowed = false;

	public float eatTime, foodValue, effectValue;
	public string effect;

	public Collider visionCollider;
	public List<IConsumable> foodInRange = new List<IConsumable>();

	private float timeSinceLastIncrease = 0f;
	public Animator animator;
	private int lastPlayedIndex = -1;
	private Coroutine coroutine;

	private void Update()
	{
		timeSinceLastIncrease += Time.deltaTime;

		if (timeSinceLastIncrease >= 60f)
		{
			IncreaseEatingSpeed();
			timeSinceLastIncrease = 0f;
		}

		if(slowTimer >= slowDuration)
		{
			isSlowed = false;
		}
		else
		{
			slowTimer += 1 * Time.deltaTime;
		}

	}

	private void IncreaseEatingSpeed()
	{
		eatingSpeed += 0.1f;
	}

	public void AddHunger(float foodValue)
	{
		if (currentHunger < maxHunger)
		{
			currentHunger += foodValue;

		}
	}

	public void UpdateSpeed(float speed)
	{
		hungerDrainSpeed = speed;
	}

	public void Slowed()
	{
		sprite.color = Color.magenta;
		slowTimer = 0;
		slowDuration = effectValue;
		isSlowed = true;
	}

	public void PlayWalkSound()
	{
		coroutine = StartCoroutine(PlayRandomSound());
	}
	public void StopWalkSound()
	{
		StopCoroutine(coroutine);
	}
	IEnumerator PlayRandomSound()
	{
		while (true)
		{
			// Wait for 2 seconds
			yield return new WaitForSeconds(2f);

			// Get a random index that is not the same as the last played index
			int newIndex;
			do
			{
				newIndex = UnityEngine.Random.Range(0, 3);
			} while (newIndex == lastPlayedIndex);

			if (newIndex == 0)
				MusicManager.instance.soundSources[8].Play();
			else if (newIndex == 1)
				MusicManager.instance.soundSources[9].Play();
			else if (newIndex == 2)
				MusicManager.instance.soundSources[10].Play();

			// Update the last played index
			lastPlayedIndex = newIndex;
		}
	}

	public void PlayDeleteSound()
	{
		StartCoroutine(PlayDelete());
	}
	IEnumerator PlayDelete()
	{
		MusicManager.instance.soundSources[5].Play();
		yield return new WaitForSeconds(1f);
		MusicManager.instance.soundSources[5].Stop();
	}

}
