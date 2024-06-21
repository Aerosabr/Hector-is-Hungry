using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour, IConsumable
{
    public float runSpeed = 5.0f;

	public GameObject item;

	public Transform Player;
	public Transform Wolf;
	public Rigidbody2D rb;

	public void Start()
	{
		GameObject playerObj = GameObject.Find("Player");
		Player = playerObj.transform;
		GameObject wolfParentObj = GameObject.Find("Wolf");
		Wolf = wolfParentObj.transform.GetChild(3);
	}

	public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
	{
		eatTime = 50;
		foodValue = 15;
		effect = "None";
		effectValue = 0;
		Destroy(gameObject);
		Debug.Log("Consume Apple");
	}
}
