using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHighlight : MonoBehaviour
{
	public Color highlightColor = Color.yellow; // Highlight color
	public float highlightIntensity = 0.5f; // Intensity of the highlight effect

	private GameObject highlightObject;
	private SpriteRenderer highlightSpriteRenderer;
	private SpriteRenderer originalSpriteRenderer;

	void Start()
	{
		// Get the original sprite renderer
		originalSpriteRenderer = GetComponent<SpriteRenderer>();

		// Create a new GameObject as child for highlight effect
		highlightObject = new GameObject("HighlightObject");
		highlightObject.transform.SetParent(transform);
		highlightObject.transform.localPosition = Vector3.zero;

		// Add a SpriteRenderer component to the highlight object
		highlightSpriteRenderer = highlightObject.AddComponent<SpriteRenderer>();

		// Assign the original sprite to the highlight sprite renderer
		highlightSpriteRenderer.sprite = originalSpriteRenderer.sprite;

		// Set the highlight material on the highlight sprite renderer
		Material highlightMaterial = new Material(Shader.Find("Sprites/Default")); // Example shader; adjust as needed
		highlightMaterial.color = highlightColor * highlightIntensity; // Apply highlight color and intensity
		highlightSpriteRenderer.material = highlightMaterial;
	}
}