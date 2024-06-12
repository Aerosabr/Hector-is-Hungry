using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool Taken = false;
    public string Pos;

    private void Awake()
    {
        if (transform.childCount == 1)
            Taken = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Item>().CheckSlot(Pos);
    }
}
