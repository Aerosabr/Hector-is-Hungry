using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
	void Start()
	{

	}

	public void DestroyObject()
	{
		Destroy(transform.gameObject);
	}
}
