using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDataTransfer : MonoBehaviour
{
	public static VolumeDataTransfer instance;

	public float soundVolume = 0.5f;
	public float musicVolume = 0.5f;

	private void Awake()
	{
		// Ensure this object is not destroyed when loading a new scene
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Optional: Update volume values and save settings
	public void SetSoundVolume(float volume)
	{
		soundVolume = volume;
	}

	public void SetMusicVolume(float volume)
	{
		musicVolume = volume;
	}
}