using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    public static RegionManager instance;
    //Regions that have been generated in game
    [SerializeField] private List<GameObject> Regions = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadRegions();
		MeasureAndLogPerimeters();
	}

    public void LoadRegions()
    {
        int roll = 0;
        if (Random.Range(0, 100) < 20)
            roll = 1;
        GameObject region1 = Instantiate(Almanac.instance.Region1[roll], gameObject.transform);
        Regions.Add(region1);
        region1.transform.position = new Vector2(10, 0);

        GameObject region2 = Instantiate(Almanac.instance.Region2[Random.Range(0, Almanac.instance.Region2.Count)], gameObject.transform);
        Regions.Add(region2);
        region2.transform.position = new Vector2(20, 0);

        GameObject region3 = Instantiate(Almanac.instance.Region3[Random.Range(0, Almanac.instance.Region3.Count)], gameObject.transform);
        Regions.Add(region3);
        region3.transform.position = new Vector2(30, 0);
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
