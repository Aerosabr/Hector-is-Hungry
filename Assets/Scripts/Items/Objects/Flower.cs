using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Flower : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private GameObject InventoryImage;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private int current;

    private float GetDivisors()
    {
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        return corners[2].x - corners[1].x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        if (temp.y >= 0) //Top
            current = 1;
        else //Bottom
            current = 2;

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float divisor = GetDivisors() / 2;
        if (current == 1)
            transform.position = Input.mousePosition - new Vector3(0, divisor);
        else
            transform.position = Input.mousePosition - new Vector3(0, -divisor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[1].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[1].transform.position.y) / 2));
            image.raycastTarget = true;
            foreach (GameObject slot in Slots)
                slot.GetComponent<InventorySlot>().Taken = true;
        }
    }

    public override bool CheckSlot(string Pos)
    {
        if (!Inventory.instance.Grid[Pos].Taken)
        {
            int x = int.Parse(Pos.Substring(0, 1));
            int y = int.Parse(Pos.Substring(1, 1));
            if (current == 2)
                x -= 1;

            if (x != 1 && x != 2)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Slots[0] = Inventory.instance.Grid[x.ToString() + y.ToString()].gameObject;
                Slots[1] = Inventory.instance.Grid[(x + 1).ToString() + y.ToString()].gameObject;
                return true;
            }
        }

        return false;
    }

    public bool CheckGrid(int x, int y)
    {
        if (Inventory.instance.Grid[x.ToString() + y.ToString()].Taken)
            return false;

        if (Inventory.instance.Grid[(x + 1).ToString() + y.ToString()].Taken)
            return false;

        return true;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                if (CheckSlot(i.ToString() + j.ToString()))
                {
                    isDropped = false;
                    transform.SetParent(GameObject.Find("InventoryImages").transform);
                    OnEndDrag(null);
                    InventoryImage.SetActive(false);
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
        InventoryImage.SetActive(true);
        image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        Slots[0] = null;
        Slots[1] = null;
        current = 0;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        transform.position = GameObject.Find("Player").transform.position;
        transform.localScale = Vector3.one;
    }

    public override void Highlight(bool toggle)
    {
        if (toggle)
            HighlightObject.SetActive(true);
        else
            HighlightObject.SetActive(false);
    }

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
    {
        eatTime = 75;
        foodValue = 10;
        effect = "Poison";
        effectValue = 5;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Flower");
    }
}
