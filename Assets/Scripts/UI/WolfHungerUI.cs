using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfHungerUI : MonoBehaviour
{
    public static WolfHungerUI instance;
    public Wolf wolf;
    public Slider hungerBar;
    public Transform Icon;

    void Start()
    {
        instance = this;
    }

    public void SwitchIcon(Sprite icon)
    {
        this.Icon.GetComponent<Image>().sprite = icon;
    }    

	void Update()
	{
		hungerBar.value = wolf.currentHunger / wolf.maxHunger;

        if((wolf.currentHunger / wolf.maxHunger) < 0.4f)
        {
			float rotationSpeedFactor = Mathf.Clamp01((wolf.currentHunger / wolf.maxHunger));
            if (wolf.currentHunger != 0)
            {
                float rotationSpeed = 10 * ((wolf.maxHunger - wolf.currentHunger) / wolf.maxHunger);

                float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * (1 - wolf.currentHunger / wolf.maxHunger) * 10f;

                Icon.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            }
		}
	}


}
