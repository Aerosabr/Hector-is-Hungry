using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlideAnimation : MonoBehaviour
{
	// Start is called before the first frame update
	public RectTransform panelToAnimate; // Reference to the panel RectTransform
	public float speed = 0.5f; // Duration of the animation
	public float DLPos = 0f;
	public float URPos = 0f;

	public void SlideDown()
	{
		StartCoroutine(RollPanelDown());
	}
	public void SlideUp()
	{
		StartCoroutine(RollPanelUp());
	}

	IEnumerator RollPanelDown()
	{
		Vector2 startPos = panelToAnimate.anchoredPosition;
		float elapsedTime = 0f;

		while (elapsedTime < speed)
		{
			float newY = Mathf.Lerp(startPos.y, DLPos, (elapsedTime / speed));
			panelToAnimate.anchoredPosition = new Vector2(startPos.x, newY);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Ensure panel reaches the exact target position
		panelToAnimate.anchoredPosition = new Vector2(startPos.x, DLPos);
	}

	IEnumerator RollPanelUp()
	{
		Vector2 startPos = panelToAnimate.anchoredPosition;
		float elapsedTime = 0f;

		while (elapsedTime < speed)
		{
			float newY = Mathf.Lerp(startPos.y, URPos, (elapsedTime / speed));
			panelToAnimate.anchoredPosition = new Vector2(startPos.x, newY);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Ensure panel reaches the exact target roll-up position
		panelToAnimate.anchoredPosition = new Vector2(startPos.x, URPos);
	}

	IEnumerator RollPanelLeft()
	{
		Vector2 startPos = panelToAnimate.anchoredPosition;
		float elapsedTime = 0f;

		while (elapsedTime < speed)
		{
			float newX = Mathf.Lerp(startPos.x, DLPos, (elapsedTime / speed));
			panelToAnimate.anchoredPosition = new Vector2(newX, startPos.y);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Ensure panel reaches the exact target roll-up position
		panelToAnimate.anchoredPosition = new Vector2(DLPos, startPos.y);
	}

	IEnumerator RollPanelRight()
	{
		Vector2 startPos = panelToAnimate.anchoredPosition;
		float elapsedTime = 0f;

		while (elapsedTime < speed)
		{
			float newX = Mathf.Lerp(startPos.x, URPos, (elapsedTime / speed));
			panelToAnimate.anchoredPosition = new Vector2(newX, startPos.y);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Ensure panel reaches the exact target roll-up position
		panelToAnimate.anchoredPosition = new Vector2(URPos, startPos.y);
	}
}
