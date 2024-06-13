using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeItem
{
    public GameObject Item;
    public int x;
    public int y;

    public LargeItem (GameObject item, int x, int y)
    {
        Item = item;
        this.x = x;
        this.y = y;
    }
}
