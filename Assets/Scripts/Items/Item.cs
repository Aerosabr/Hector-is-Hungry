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

    public abstract void CheckSlot(string Pos);
    public abstract bool PickupItem();
    public void ItemDropped()
    {
        sprite.enabled = true;
        image.enabled = false;
        box.enabled = true;
    }
}
