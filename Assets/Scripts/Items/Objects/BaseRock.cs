using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseRock : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject Controller;
    public int Pos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        Controller.GetComponent<Rock>().BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Controller.GetComponent<Rock>().Dragging(Pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public override void CheckSlot(string Pos)
    {
        Controller.GetComponent<Rock>().CheckSlot(Pos, this.Pos);
    }

    public override bool PickupItem()
    {
        throw new System.NotImplementedException();
    }
}
