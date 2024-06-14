using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private List<GameObject> nearbyObjects = new List<GameObject>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "Item")
            nearbyObjects.Add(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.tag == "Item")
            nearbyObjects.Remove(collision.gameObject);
    }

    public void OnPickup()
    {
        if (nearbyObjects.Count > 1)
        {
            float distance = Mathf.Infinity;
            int index = 0;
            for (int i = 0; i < nearbyObjects.Count; i++)
            {
                if (Vector3.Distance(transform.position, nearbyObjects[i].transform.position) < distance)
                {
                    
                    distance = Vector3.Distance(transform.position, nearbyObjects[i].transform.position);
                    index = i;
                }
            }
            nearbyObjects[index].GetComponent<Item>().PickupItem();
        }
        else if (nearbyObjects.Count == 1)
            nearbyObjects[0].GetComponent<Item>().PickupItem();
    }
}
