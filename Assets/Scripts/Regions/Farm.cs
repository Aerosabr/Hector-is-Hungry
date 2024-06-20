using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Region
{
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnX;
    [SerializeField] private float spawnY;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(spawnArea.position, new Vector3(spawnX, spawnY));
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (numActive < 6 && Spawnable)
        {
            Spawnable = false;
            StartCoroutine(SpawnItem());
        }
    }

    private IEnumerator SpawnItem()
    {
        GameObject item = Items[Random.Range(0, Items.Count)];
        yield return new WaitForSeconds(item.GetComponent<Item>().spawnDuration);
        Spawning(item);
        Spawnable = true;
    }

    private void Spawning(GameObject item)
    {
        bool searchLocation = true;
        Vector3 SpawnArea = Vector3.zero;
        while (searchLocation)
        {
            searchLocation = false;
            SpawnArea = new Vector3(Random.Range(-spawnX / 2, spawnX / 2), Random.Range((-spawnY / 2) - 2, (spawnY / 2) - 2));
            Collider2D[] colliders = Physics2D.OverlapCircleAll(SpawnArea, 5f);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.name);
                if (((1 << collider.gameObject.layer) & unspawnableLayers) != 0)
                {
                    searchLocation = true;
                    break;
                }
            }
        }
        GameObject temp = Instantiate(item, transform);
        temp.transform.localPosition = SpawnArea;
        numActive++;
    }

}
