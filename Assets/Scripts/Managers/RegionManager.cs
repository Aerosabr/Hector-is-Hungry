using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    //Regions that have been generated in game
    [SerializeField] private List<GameObject> Regions = new List<GameObject>();

    private void Awake()
    {
        
    }

    private void Start()
    {
        LoadRegions();
    }

    public void LoadRegions()
    {
        Regions.Add(Instantiate(Almanac.instance.Region1[Random.Range(0, Almanac.instance.Region1.Count)], new Vector2(0, 0), Quaternion.identity));
        Regions.Add(Instantiate(Almanac.instance.Region2[Random.Range(0, Almanac.instance.Region2.Count)], new Vector2(10, 0), Quaternion.identity));
        Regions.Add(Instantiate(Almanac.instance.Region3[Random.Range(0, Almanac.instance.Region3.Count)], new Vector2(20, 0), Quaternion.identity));
    }
}
