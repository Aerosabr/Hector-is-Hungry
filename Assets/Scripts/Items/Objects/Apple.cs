using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Apple : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
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
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public override void CheckSlot(string Pos)
    {
        if (!Inventory.instance.Grid[Pos].Taken)
        {
            parentAfterDrag = Inventory.instance.Grid[Pos].gameObject.transform;
            Inventory.instance.Grid[Pos].Taken = true;
        }
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                if (!Inventory.instance.Grid[i.ToString() + j.ToString()].Taken)
                {
                    InventorySlot openSlot = Inventory.instance.Grid[i.ToString() + j.ToString()];
                    transform.SetParent(openSlot.transform);
                    sprite.enabled = false;
                    image.enabled = true;
                    box.enabled = false;
                    return true;
                }
            }
        }
        return false;
    }
}
