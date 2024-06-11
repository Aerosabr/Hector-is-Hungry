using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfHungerUI : MonoBehaviour
{
    public static WolfHungerUI instance;
    public Slider hungerBar;
    public Transform Icon;


    public float hungerCapacity = 100;
    public float currentHunger = 100;
    public float hungerSpeed = 1f;

    void Start()
    {
        instance = this;
        currentHunger = 100;
    }

    public void AddHunger(float foodValue)
    {
        currentHunger += foodValue;
    }

    public void UpdateSpeed(float speed)
    {
        this.hungerSpeed = speed;
    }

    public void SwitchIcon(Sprite icon)
    {
        this.Icon.GetComponent<Image>().sprite = icon;
    }    

	void Update()
	{
		currentHunger -= (hungerSpeed / 1000) + Time.deltaTime;
		hungerBar.value = currentHunger / hungerCapacity;

        if((currentHunger/hungerCapacity) < 0.4f)
        {
			float rotationSpeedFactor = Mathf.Clamp01((currentHunger / hungerCapacity));
            if (currentHunger != 0)
            {
                float rotationSpeed = 10 * ((hungerCapacity - currentHunger) / hungerCapacity);

                float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * (1 - currentHunger / hungerCapacity) * 10f;

                Icon.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            }
		}
	}


}
