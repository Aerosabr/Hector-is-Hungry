using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Vector3 targetPoint = Vector3.zero;
	public Player player; // Assign in Inspector
	private Rigidbody2D rb;

	public float moveSpeed = 3.0f;
	public float lookAheadDistance = 5f;
	public float lookAheadSpeed = 3f;
	private float lookOffset = 0f;

	private float minX = -9.2f;
	private float maxX = 39.2f;
	void Start()
	{
		rb = player.GetComponent<Rigidbody2D>(); // Assuming player has Rigidbody2D
		targetPoint = player.transform.position - Vector3.forward * 10f; // Adjust the offset as needed
	}

	void FixedUpdate()
	{
		// Adjust lookOffset based on player's horizontal velocity
		if (rb.velocity.x > 0f)
		{
			lookOffset = lookAheadDistance;
		}
		else if (rb.velocity.x < 0f)
		{
			lookOffset = -lookAheadDistance;
		}

		// Update targetPoint based on player's position and lookOffset
		targetPoint.x = player.transform.position.x + lookOffset;
	}

	void LateUpdate()
	{
		if (player == null)
			return;
		float targetViewportX = Camera.main.WorldToViewportPoint(player.transform.position).x;
		if (targetViewportX < 0.4f || targetViewportX > 0.6f)
		{
			// Smoothly move the camera towards targetPoint
			Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);

			// Ensure camera stays within horizontal boundaries
			desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

			transform.position = desiredPosition;
		}
	}
}


