using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
	private SpriteRenderer sprite;
	private float minAlpha = 25f; // Minimum alpha value
	private float maxAlpha = 125f; // Maximum alpha value
	private float duration = 2f; // Duration for the transparency change (seconds)
	private float startTime;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		startTime = Time.time; // Record the start time
	}

	void Update()
	{
		// Calculate the current time fraction
		float timeFraction = (Time.time - startTime) / duration;

		// Apply boomerang effect to alpha (lerp from minAlpha to maxAlpha and back)
		float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(duration, 1f));

		// Get current color and set alpha
		Color spriteColor = sprite.color;
		spriteColor.a = alpha;

		// Apply the modified color back to the sprite renderer
		sprite.color = spriteColor;
	}
}
