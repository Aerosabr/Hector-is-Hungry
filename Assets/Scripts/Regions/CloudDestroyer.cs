using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && collision.GetComponent<Cloud>() != null)
        {
            Debug.Log("Destroyed " + collision.name);
            Destroy(collision.gameObject);
        }
    }
}
