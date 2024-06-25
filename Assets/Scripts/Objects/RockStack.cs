using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockStack : MonoBehaviour
{
    [SerializeField] private List<Sprite> Rocks = new List<Sprite>();
    [SerializeField] private List<GameObject> CachedRocks = new List<GameObject>();
    [SerializeField] private SpriteRenderer StackOfRocks;
    [SerializeField] private GameObject Built;
    [SerializeField] private GameObject Unbuilt;
    [SerializeField] private int numRocks;
    [SerializeField] private GameObject Bubble;
    [SerializeField] private TMPro.TextMeshPro BubbleText;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && numRocks < 3 && collision.gameObject.GetComponent<Rock>() != null && collision.gameObject.GetComponent<Item>().isDropped)
        {
            Debug.Log("Rock stacked");
            numRocks++;
            BubbleText.text = numRocks + "/3";
            Bubble.SetActive(true);
            CachedRocks.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Item>().region.numActive--;
            StackOfRocks.sprite = Rocks[numRocks - 1];
            if (numRocks == 3)
            {
                Built.SetActive(true);
                Unbuilt.SetActive(false);
                Bubble.SetActive(false);
            }
        }
    }

}
