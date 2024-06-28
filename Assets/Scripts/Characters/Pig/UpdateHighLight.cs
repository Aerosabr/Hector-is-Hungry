using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHighLight : MonoBehaviour
{
	[SerializeField] private SpriteRenderer original;
	[SerializeField] private SpriteRenderer copy;

	// Update is called once per frame
	void Update()
    {
        copy.sprite = original.sprite;
    }
}
