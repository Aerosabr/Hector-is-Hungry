using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public static ItemDrop instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Item>().ItemDropped();
        eventData.pointerDrag.GetComponent<Item>().parentAfterDrag = RegionManager.instance.transform;
        eventData.pointerDrag.GetComponent<Item>().transform.position = GameObject.Find("Player").transform.position;
    }

}
