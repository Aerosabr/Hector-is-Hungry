using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Sprite ladder;
    [SerializeField] private List<GameObject> CachedItems = new List<GameObject>();
    [SerializeField] private GameObject Built;
    [SerializeField] private GameObject Unbuilt;
    [SerializeField] private int numVines = 0;
    [SerializeField] private int numSticks = 0;
    [SerializeField] private GameObject BubbleWood;
    [SerializeField] private GameObject BubbleVines;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && numVines < 1 && collision.gameObject.GetComponent<Vines>() != null && collision.gameObject.GetComponent<Item>().isDropped)
        {
            Debug.Log("Vines stacked");
            numVines++;
            BubbleWood.SetActive(true);
            CachedItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Item>().region.numActive--;

            if (numSticks == 1)
            {
                Built.SetActive(true);
                GetComponent<SpriteRenderer>().sprite = ladder;
                Unbuilt.SetActive(false);
                BubbleWood.SetActive(false);
                BubbleVines.SetActive(false);
            }
        }
        else if (collision.tag == "Item" && numSticks < 1 && collision.gameObject.GetComponent<Sticks>() != null && collision.gameObject.GetComponent<Item>().isDropped)
        {
            Debug.Log("Sticks stacked");
            numSticks++;
            CachedItems.Add(collision.gameObject);
            BubbleVines.SetActive(true);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Item>().region.numActive--;

            if (numVines == 1)
            {
                Built.SetActive(true);
                Unbuilt.SetActive(false);
                GetComponent<SpriteRenderer>().sprite = ladder;
                BubbleVines.SetActive(false);
                BubbleWood.SetActive(false);
            }
        }
    }
}
