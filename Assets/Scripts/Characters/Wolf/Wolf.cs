using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wolf : MonoBehaviour
{

    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float currentHunger = 100;
    [SerializeField] private float hungerDrainSpeed = 1;
    [SerializeField] private float eatingSpeed = 1;
    [SerializeField] private float stunTimer = 0;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
