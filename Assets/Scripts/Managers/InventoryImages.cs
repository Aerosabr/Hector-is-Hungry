using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryImages : MonoBehaviour
{
    public static InventoryImages instance;

    private void Awake()
    {
        instance = this;
    }


}
