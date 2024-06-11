using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateSoundVolume()
    {
        //GameManager.instance.Sound = soundSlider.value;
    }

    public void UpdateMusicVolume()
    {
		//GameManager.instance.Music = musicSlider.value;
	}
}
