using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRegion : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			MusicManager.instance.musicSources[2].Play();
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			MusicManager.instance.musicSources[2].Stop();
		}
	}
}

