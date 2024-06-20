using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public int numActive;
    public List<GameObject> Items = new List<GameObject>();
    public LayerMask unspawnableLayers;
    public bool Spawnable = true;
}