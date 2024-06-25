using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Sprite ladder;
    [SerializeField] private List<GameObject> CachedItems = new List<GameObject>();
    [SerializeField] private GameObject Built;
    [SerializeField] private GameObject Unbuilt;
    [SerializeField] private int numSticks = 0;
    [SerializeField] private GameObject BubbleWood;
    [SerializeField] private TMPro.TextMeshPro BubbleText;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && numSticks < 3 && collision.gameObject.GetComponent<Sticks>() != null && collision.gameObject.GetComponent<Item>().isDropped)
        {
            Debug.Log("Sticks stacked");
            numSticks++;
            BubbleText.text = numSticks + "/3";
            CachedItems.Add(collision.gameObject);
            BubbleWood.SetActive(true);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Item>().region.numActive--;

            if (numSticks == 3)
            {
                Built.SetActive(true);
                Unbuilt.SetActive(false);
                GetComponent<SpriteRenderer>().sprite = ladder;
                BubbleWood.SetActive(false);
            }
        }
    }
}
