using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class House : MonoBehaviour
{
	[SerializeField] private GameObject pig;
    [SerializeField] private string material;
	[SerializeField] private List<Sprite> sprite_List = new List<Sprite>();
	[SerializeField] private int currentAmount;
    [SerializeField] private int requireAmount;
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private TextMeshPro text;
	[SerializeField] private GameObject bubble;
	[SerializeField] private Victory victory;
	// Start is called before the first frame update
	void Start()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Item" && currentAmount < requireAmount)
		{
			//Debug.Log(collision.gameObject.name);

			// Check if the collision object has a "haybale" or "apple" script
			Haybale haybaleScript = collision.gameObject.GetComponent<Haybale>();
			Sticks stickScript = collision.gameObject.GetComponent<Sticks>();
			Rock rockScript = collision.gameObject.GetComponent<Rock>();
			Meteor meteorScript = collision.gameObject.GetComponent<Meteor>();
			Tumbleweed tumbleweedScript = collision.gameObject.GetComponent<Tumbleweed>();

			if(material == "Haybale" && (haybaleScript != null || tumbleweedScript != null))
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
                collision.GetComponent<Item>().region.numActive--;
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if(currentAmount == requireAmount) 
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					//Instantiate(pig, transform.position, Quaternion.identity);
					MusicManager.instance.soundSources[14].Play();
					pig.transform.GetComponent<Pig>().Activate();
				}
			}
			else if (material == "Sticks" && stickScript != null)
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
                collision.GetComponent<Item>().region.numActive--;
                collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					//Instantiate(pig, transform.position, Quaternion.identity);
					MusicManager.instance.soundSources[14].Play();
					pig.transform.GetComponent<Pig>().Activate();
				}
			}
			else if (material == "Rock" && (rockScript != null || meteorScript != null))
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
                collision.GetComponent<Item>().region.numActive--;
                collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					MusicManager.instance.soundSources[14].Play();
					victory.VictoryScreen();
				}
			}
		}
	}

	public void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Item" && currentAmount < requireAmount)
		{
			//Debug.Log(collision.gameObject.name);

			// Check if the collision object has a "haybale" or "apple" script
			Haybale haybaleScript = collision.gameObject.GetComponent<Haybale>();
			Sticks stickScript = collision.gameObject.GetComponent<Sticks>();
			Rock rockScript = collision.gameObject.GetComponent<Rock>();
			Meteor meteorScript = collision.gameObject.GetComponent<Meteor>();

			if (material == "Haybale" && haybaleScript != null)
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
				collision.GetComponent<Item>().region.numActive--;
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					//Instantiate(pig, transform.position, Quaternion.identity); Activate pig here
					pig.transform.GetComponent<Pig>().Activate();
				}
            }
			else if (material == "Sticks" && stickScript != null)
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
				collision.GetComponent<Item>().region.numActive--;
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					//Instantiate(pig, transform.position, Quaternion.identity); Activate pig here
					pig.transform.GetComponent<Pig>().Activate();
				}
            }
			else if (material == "Rock" && (rockScript != null || meteorScript != null))
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
				collision.GetComponent<Item>().region.numActive--;
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					victory.VictoryScreen();
				}
			}
		}
	}
}
