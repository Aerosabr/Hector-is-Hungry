using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionManager : MonoBehaviour
{
    public static RegionManager instance;
    [SerializeField] private List<Sprite> BuildMaterials = new List<Sprite>();
    [SerializeField] private SpriteRenderer HouseHay;
    [SerializeField] private SpriteRenderer HouseBrick;
    [SerializeField] private House BrickHouse;
    [SerializeField] private TMPro.TextMeshPro BrickQuantity;
    //Regions that have been generated in game
    [SerializeField] private List<GameObject> Regions = new List<GameObject>();
    [SerializeField] private GameObject Cloud;
    [SerializeField] private GameObject CloudDestroyer;
    [SerializeField] private bool Space;
    [SerializeField] private bool Tutorial;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!Tutorial)
        {
            LoadRegions();
            StartCoroutine(SpawnClouds());
        }
		MeasureAndLogPerimeters();

	}

    public IEnumerator SpawnClouds()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        GameObject cloud = Instantiate(Cloud, transform);
        if (Space)        
            cloud.transform.localPosition = new Vector3(Random.Range(-22f, 36f), Random.Range(4f, 6.4f));      
        else     
            cloud.transform.localPosition = new Vector3(Random.Range(-22f, 51f), Random.Range(4f, 6.4f));

        if (cloud.transform.localPosition.x < 0)
            cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        else
            cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);

        StartCoroutine(SpawnClouds());
    }

    public void LoadRegions()
    {
        GameObject cloudDestroyer = Instantiate(CloudDestroyer, transform);
        cloudDestroyer.transform.position = new Vector2(-30, 0);
        int roll = 0;
        if (Random.Range(0, 100) < 20)
        {
            roll = 1;
            HouseHay.sprite = BuildMaterials[0];
        } 
        GameObject region1 = Instantiate(Almanac.instance.Region1[roll], gameObject.transform);
        Regions.Add(region1);
        region1.transform.position = new Vector2(15, 0);

        GameObject region2 = Instantiate(Almanac.instance.Region2[Random.Range(0, Almanac.instance.Region2.Count)], gameObject.transform);
        Regions.Add(region2);
        region2.transform.position = new Vector2(30, 0);

        int r3 = Random.Range(0, Almanac.instance.Region3.Count);
        if (r3 == 2)
        {
            HouseBrick.sprite = BuildMaterials[1];
            BrickQuantity.text = "0/2";
            BrickHouse.requireAmount = 2;
            Space = true;
            GameObject cloudDestroyer2 = Instantiate(CloudDestroyer, transform);
            cloudDestroyer2.transform.position = new Vector2(45, 0);
        }
        else
        {
            GameObject cloudDestroyer2 = Instantiate(CloudDestroyer, transform);
            cloudDestroyer2.transform.position = new Vector2(60, 0);
        }
        GameObject region3 = Instantiate(Almanac.instance.Region3[r3], gameObject.transform);
        Regions.Add(region3);
        region3.transform.position = new Vector2(45, 0);
    }

	private void MeasureAndLogPerimeters()
	{
		float totalPerimeter = 0f;

		foreach (GameObject region in Regions)
		{
			// Example: Assuming each region has an EdgeCollider2D to represent its perimeter
			EdgeCollider2D collider = region.GetComponent<EdgeCollider2D>();
			if (collider != null)
			{
				float perimeter = CalculatePerimeter(collider); // Calculate perimeter based on collider points
				totalPerimeter += perimeter;

				Debug.Log($"Region {region.name} Perimeter: {perimeter}");
			}
			else
			{
				Debug.LogWarning($"Region {region.name} does not have an EdgeCollider2D for perimeter measurement.");
			}
		}

		Debug.Log($"Total Perimeter of All Regions: {totalPerimeter}");
	}

	private float CalculatePerimeter(EdgeCollider2D collider)
	{
		float perimeter = 0f;

		// Loop through each edge segment and calculate length
		for (int i = 0; i < collider.pointCount - 1; i++)
		{
			Vector2 point1 = collider.points[i];
			Vector2 point2 = collider.points[i + 1];

			perimeter += Vector2.Distance(point1, point2);
		}

		// Add the distance from the last point to the first point to close the loop
		perimeter += Vector2.Distance(collider.points[collider.pointCount - 1], collider.points[0]);

		return perimeter;
	}
}
