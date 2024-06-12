using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;

    public Player player;
	//Sprite Movement
	[SerializeField] private Rigidbody2D rb;

	public float moveSpeed = 3.0f;

    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;

    public float lookOffset;

	public float reCalculationTimer = 3f;

	private Coroutine coroutine;

	public bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        rb = player.transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

		Vector3 targetViewportPoint = Camera.main.WorldToViewportPoint(targetPoint);

		if (rb.velocity.x > 0f)
		{
			lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
		}
		else if (rb.velocity.x < 0f)
		{
			lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
		}

		targetPoint.x = player.transform.position.x + lookOffset;
		if (targetViewportPoint.x < -0.1 || targetViewportPoint.x > 1.1)
		{
			transform.position = Vector3.Lerp(transform.position, targetPoint + new Vector3(5, 0 , 0), moveSpeed * Time.deltaTime);
		}

		//if(first)
		//{
		//	transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
		//}

		//else
		//{
		//	// Check if targetPoint is outside of the camera's view
		//	Debug.Log("ViewPort" + targetViewportPoint.x);
		//	if (targetViewportPoint.x < -0.1 || targetViewportPoint.x > 1.5)
		//	{
		//		if (coroutine != null)
		//		{
		//			StopCoroutine(coroutine);
		//		}
		//		coroutine = StartCoroutine(GetToPoint(transform, targetPoint));
		//	}
		//}


	}

	IEnumerator GetToPoint(Transform current, Vector3 target)
	{
		float Timer = 0f;
		while(Vector3.Distance(current.position, target) > 0.1 && Timer < reCalculationTimer) 
		{
			Debug.Log("Distance" + Vector3.Distance(transform.position, target) + "Timer: " + Timer);
			Timer += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
			yield return null;
		}
		yield return null;
	}
}
