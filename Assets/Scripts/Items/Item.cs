using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Image image;
    public SpriteRenderer sprite;
    public BoxCollider2D box;
    public Transform parentAfterDrag;
    public bool isDropped;

    public abstract bool CheckSlot(string Pos);
    public abstract bool PickupItem();
    public abstract void ItemDropped();
    public abstract void Consume();
}
