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

	[SerializeField] private Camera mainCamera;
	[SerializeField] private float minX = -9.2f;
	private float maxX = 39.2f;
	[SerializeField] private Transform minimumX;
	private bool isMovingAwayFromMinX = false;

	void Start()
	{
		rb = player.GetComponent<Rigidbody2D>(); // Assuming player has Rigidbody2D
		targetPoint = player.transform.position - Vector3.forward; // Adjust the offset as needed
	}

	void FixedUpdate()
	{
		// Adjust lookOffset based on player's horizontal velocity
		if (rb != null)
		{
			if (minX >= 25.7)
				maxX = 64.0f;

			minX = minimumX.position.x + 13.57f;

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
	}

	void LateUpdate()
	{
		if (player == null)
			return;

		float targetViewportX = mainCamera.WorldToViewportPoint(player.transform.position).x;
		Vector3 minXWorldPosition = new Vector3(minX, player.transform.position.y, player.transform.position.z);
		float minXViewportX = mainCamera.WorldToViewportPoint(minXWorldPosition).x;

		// Check if minX is within camera's view
		if (minXViewportX > 0.4f && minXViewportX < 0.6f)
		{
			isMovingAwayFromMinX = true;
		}

		if (isMovingAwayFromMinX)
		{
			// Smoothly move the camera away from minX
			Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
			desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

			// Stop moving away from minX once it's no longer in the view
			if (minXViewportX < 5f || minXViewportX > 0f)
			{
				isMovingAwayFromMinX = false;
			}

			transform.position = desiredPosition;
		}
		else if (targetViewportX < 0.4f || targetViewportX > 0.6f)
		{
			// Smoothly move the camera towards targetPoint
			Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);

			// Ensure camera stays within horizontal boundaries
			desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

			transform.position = desiredPosition;
		}
	}
}


