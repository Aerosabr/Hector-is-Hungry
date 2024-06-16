using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
	public float hoverHeight = 0.5f; // Adjust this value to control the height of hovering
	public float hoverSpeed = 1f; // Adjust this value to control the speed of hovering

	private Vector3 startPos;

	void Start()
	{
		startPos = transform.localPosition; // Use localPosition to start from the current position
	}

	void Update()
	{
		// Calculate position offset based on sine wave for smooth hovering
		float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

		// Apply the offset to the object's local Y position
		transform.localPosition = startPos + new Vector3(0, hoverOffset, 0);
	}
}
