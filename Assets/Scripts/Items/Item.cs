using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Image image;
    public BoxCollider2D box;
    public bool isDropped;
    public int xSize;
    public int ySize;
    public abstract bool CheckSlot(string Pos);
    public abstract bool PickupItem();
    public abstract void ItemDropped();
    public abstract void Highlight(bool toggle);
}
