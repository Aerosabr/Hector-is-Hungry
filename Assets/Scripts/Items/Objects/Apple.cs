using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Apple : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        parentAfterDrag.gameObject.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            parentAfterDrag.GetComponent<InventorySlot>().Taken = true;
        }
    }

    public override bool CheckSlot(string Pos)
    {
        if (!Inventory.instance.Grid[Pos].Taken)
        {
            parentAfterDrag = Inventory.instance.Grid[Pos].gameObject.transform;
            return true;
        }

        return false;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                if (!Inventory.instance.Grid[i.ToString() + j.ToString()].Taken)
                {
                    isDropped = false;
                    InventorySlot openSlot = Inventory.instance.Grid[i.ToString() + j.ToString()];
                    transform.SetParent(openSlot.transform);
                    openSlot.Taken = true;
                    sprite.enabled = false;
                    image.enabled = true;
                    box.enabled = false;
                    transform.localScale = new Vector3(1, 1, 1);
                    return true;
                }
            }
        }
        return false;
    }

    public override void ItemDropped()
    {
        sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        parentAfterDrag = RegionManager.instance.transform;
        transform.position = GameObject.Find("Player").transform.position;
    }

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
    {
        eatTime = 50;
        foodValue = 15;
        effect = "None";
        effectValue = 0;
        Destroy(gameObject);
        Debug.Log("Consume Apple");
    }
}
