using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<string, InventorySlot> Grid = new Dictionary<string, InventorySlot>();

    private void Awake()
    {
        instance = this;
        int x = 1, y = 1;

        for (int i = 0; i < 9; i++)
        {
            if (y == 4)
            {
                x++;
                y = 1;
            }

            Grid.Add(x.ToString() + y.ToString(), transform.GetChild(i).GetComponent<InventorySlot>());
            transform.GetChild(i).GetComponent<InventorySlot>().Pos = x.ToString() + y.ToString();
            y++;
        }
    }
}
