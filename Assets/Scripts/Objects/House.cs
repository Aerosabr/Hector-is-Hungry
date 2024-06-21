using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
	// Start is called before the first frame update
	void Start()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Item" && currentAmount < requireAmount)
		{
			if(collision.name == material)
			{
				currentAmount += 1;
				text.text = currentAmount.ToString() + "/" + requireAmount.ToString();
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if(currentAmount == requireAmount) 
				{
					bubble.SetActive(false);
					sprite.sprite = sprite_List[1];
					Instantiate(pig, transform.position, Quaternion.identity);
				}
			}
		}
	}

	public void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Item" && currentAmount < requireAmount)
		{
			if (collision.name == material)
			{
				currentAmount += 1;
				collision.GetComponent<DestroyOnContact>().DestroyObject();
				if (currentAmount == requireAmount)
				{
					sprite.sprite = sprite_List[1];
					Instantiate(pig, transform.position, Quaternion.identity);
				}
			}
		}
	}
}
