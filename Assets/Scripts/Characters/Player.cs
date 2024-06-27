using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private List<GameObject> nearbyObjects = new List<GameObject>();
	[SerializeField] private GameObject closestItem;
	[SerializeField] private Coroutine coroutine;
    [SerializeField] private GameObject gameOver;
    public float sprintDuration = 0;
    public bool canEat;
    public bool canPickup;

	void Start()
	{
		StartCoroutine(PlayAudioWithRandomInterval());
	}
    private void FixedUpdate()
    {
        if (sprintDuration > 0)
        {
            sprintDuration -= Time.deltaTime;
            movementSpeed = 1.875f;
        }
        else
            movementSpeed = 1.25f;
    }

	public void OnDestroy()
	{
		GameObject.Find("Timer").GetComponent<Timer>().TimerActive = false;
		MusicManager.instance.soundSources[2].Play();
		gameOver.SetActive(true);
	}

	public void OnTriggerEnter2D(Collider2D collision)
    {
		//Debug.Log("Enter")
		if (collision.gameObject.tag == "Wolf")
		{
			transform.GetChild(0).gameObject.SetActive(true);
			MusicManager.instance.soundSources[13].Play();
		}
		if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "NPC")
            nearbyObjects.Add(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
		//Debug.Log("Exit");
		if (collision.gameObject.tag == "Wolf")
			transform.GetChild(0).gameObject.SetActive(false);
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "NPC")
        {
			nearbyObjects.Remove(collision.gameObject);
			Item itemComponent = collision.gameObject.GetComponent<Item>();
			if (itemComponent != null)
			{
				itemComponent.Highlight(false);
			}
		}
    }

	public void OnTriggerStay2D(Collider2D collision)
	{
		HighlightNearest();
	}

	public void OnPickup()
    {
        if (canPickup)
        {
            int index = 0;
            bool containSpace = false;
            if (nearbyObjects.Count > 1)
            {
                float distance = Mathf.Infinity;
                for (int i = 0; i < nearbyObjects.Count; i++)
                {
                    if (Vector3.Distance(transform.position, nearbyObjects[i].transform.position) < distance)
                    {

                        distance = Vector3.Distance(transform.position, nearbyObjects[i].transform.position);
                        index = i;
                    }
                }
                containSpace = nearbyObjects[index].GetComponent<Item>().PickupItem();
            }
            else if (nearbyObjects.Count == 1)
                containSpace = nearbyObjects[index].GetComponent<Item>().PickupItem();

            if (!containSpace && nearbyObjects.Count != 0)
            {
                nearbyObjects[index].transform.GetChild(1).gameObject.SetActive(true);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(DeactivateAfterDelay(nearbyObjects[index].transform.GetChild(1).gameObject, 2.0f));
            }
        }	
    }

    public void OnEat()
    {
        if (canEat)
        {
            int index = 0;
            bool containSpace = false;
            if (nearbyObjects.Count > 1)
            {
                float distance = Mathf.Infinity;
                for (int i = 0; i < nearbyObjects.Count; i++)
                {
                    if (Vector3.Distance(transform.position, nearbyObjects[i].transform.position) < distance)
                    {

                        distance = Vector3.Distance(transform.position, nearbyObjects[i].transform.position);
                        index = i;
                    }
                }
                containSpace = nearbyObjects[index].GetComponent<Item>().EatItem(this);
            }
            else if (nearbyObjects.Count == 1)
                containSpace = nearbyObjects[index].GetComponent<Item>().EatItem(this);

            if (!containSpace && nearbyObjects.Count != 0)
            {
                nearbyObjects[index].transform.GetChild(2).gameObject.SetActive(true);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(DeactivateAfterDelay(nearbyObjects[index].transform.GetChild(2).gameObject, 2.0f));
            }
        }     
    }

    public void HighlightNearest()
	{
		if(closestItem != null)
		{
			Item itemComponent = closestItem.GetComponent<Item>();
			if (itemComponent != null)
			{
				itemComponent.Highlight(false);
			}
		}
		if (nearbyObjects.Count > 0)
		{
			float distance = Mathf.Infinity;
			int index = 0;
			for (int i = 0; i < nearbyObjects.Count; i++)
			{
				if (nearbyObjects[i])
				{
					if (Vector3.Distance(transform.position, nearbyObjects[i].transform.position) < distance)
					{

						distance = Vector3.Distance(transform.position, nearbyObjects[i].transform.position);
						index = i;
					}
				}
			}
			closestItem = nearbyObjects[index];
			Item itemComponent = closestItem.GetComponent<Item>();
			if (itemComponent != null)
			{
				itemComponent.Highlight(true);
			}
		}
	}
	IEnumerator PlayAudioWithRandomInterval()
	{
		while (true)
		{
			// Wait for a random time between 15 and 20 seconds
			float waitTime = Random.Range(10f, 20f);
			yield return new WaitForSeconds(waitTime);

			// Play the audio source
			MusicManager.instance.soundSources[1].Play();
		}
	}

	private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
	{
		// Wait for 'delay' seconds
		yield return new WaitForSeconds(delay);

		// Deactivate the GameObject
		obj.SetActive(false);
	}
}
