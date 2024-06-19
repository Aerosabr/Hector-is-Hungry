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
    }

    public void LoadRegions()
    {
        GameObject region1 = Instantiate(Almanac.instance.Region1[Random.Range(0, Almanac.instance.Region1.Count)], gameObject.transform);
        Regions.Add(region1);
        region1.transform.position = new Vector2(10, 0);

        GameObject region2 = Instantiate(Almanac.instance.Region2[Random.Range(0, Almanac.instance.Region2.Count)], gameObject.transform);
        Regions.Add(region2);
        region2.transform.position = new Vector2(20, 0);

        GameObject region3 = Instantiate(Almanac.instance.Region3[Random.Range(0, Almanac.instance.Region3.Count)], gameObject.transform);
        Regions.Add(region3);
        region3.transform.position = new Vector2(30, 0);
    }
}
