using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public static ItemDrop instance;
    public GameObject Player;

    private void Awake()
    {
        instance = this;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Item>().ItemDropped(Player);
    }

}
