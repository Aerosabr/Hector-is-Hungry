using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desert : Region
{
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnX;
    [SerializeField] private float spawnY;
    [SerializeField] private int spawnLocation = 1;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(spawnArea.position, new Vector3(spawnX, spawnY));
    }

    private void Start()
    {
        Spawning(Items[Random.Range(0, Items.Count)]);
        Spawning(Items[Random.Range(0, Items.Count)]);
    }

    private void FixedUpdate()
    {
        if (numActive < maxActive && Spawnable)
        {
            Spawnable = false;
            StartCoroutine(SpawnItem());
        }
    }

    private IEnumerator SpawnItem()
    {
        GameObject item = Items[Random.Range(0, Items.Count)];
        float spawnDur = Random.Range(item.GetComponent<Item>().spawnDuration - 3, item.GetComponent<Item>().spawnDuration + 3);
        yield return new WaitForSeconds(spawnDur);
        if (numActive < maxActive)
            Spawning(item);
        Spawnable = true;
    }

    private void Spawning(GameObject item)
    {
        float x, y;
        switch (spawnLocation)
        {
            case 1:
                x = Random.Range(-spawnX / 2, 0);
                y = Random.Range(0, (spawnY / 2) - 3);
                break;
            case 2:
                x = Random.Range(0, spawnX / 2);
                y = Random.Range(0, (spawnY / 2) - 3);
                break;
            case 3:
                x = Random.Range(-spawnX / 2, 0);
                y = Random.Range((-spawnY / 2) - 3, 0);
                break;
            case 4:
                x = Random.Range(0, spawnX / 2);
                y = Random.Range((-spawnY / 2) - 3, 0);
                break;
            default:
                x = 0;
                y = 0;
                break;
        }
        Vector3 SpawnArea = new Vector3(x, y);

        GameObject temp = Instantiate(item, transform);
        temp.transform.localPosition = SpawnArea;
        temp.GetComponent<Item>().region = this;
        numActive++;

        if (spawnLocation == 4)
            spawnLocation = 1;
        else
            spawnLocation++;
    }
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			MusicManager.instance.musicSources[4].Play();
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			MusicManager.instance.musicSources[4].Stop();
		}
	}
}
