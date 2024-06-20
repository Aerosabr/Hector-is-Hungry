using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stalagmite : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
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
        float divisor = GetDivisors() / 4;
        if (temp.x <= 0) //Left half
        {
            if (temp.y >= divisor)
                current = 1;
            else if (temp.y >= -divisor)
                current = 3;
            else
                current = 5;
        }
        else //Right half
        {
            if (temp.y >= divisor)
                current = 2;
            else if (temp.y >= -divisor)
                current = 4;
            else
                current = 6;
        }

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float divisor = GetDivisors() / 4;
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-divisor, 2 * divisor);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(divisor, 2 * divisor);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(-divisor, 0);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(divisor, 0);
                break;
            case 5:
                transform.position = Input.mousePosition - new Vector3(-divisor, -(2 * divisor));
                break;
            case 6:
                transform.position = Input.mousePosition - new Vector3(divisor, -(2 * divisor));
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[5].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[5].transform.position.y) / 2));
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
            switch (current)
            {
                case 1:
                    break;
                case 2:
                    y -= 1;
                    break;
                case 3:
                    x -= 1;
                    break;
                case 4:
                    x -= 1;
                    y -= 1;
                    break;
                case 5:
                    x -= 2;
                    break;
                case 6:
                    x -= 2;
                    y -= 1;
                    break;
            }

            if ((y != 1 && y != 2) || x != 1)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Debug.Log("Valid");          
                int index = 0;
                for (int i = x; i <= x + 2; i++)
                {
                    for (int j = y; j <= y + 1; j++)
                    {
                        Slots[index] = Inventory.instance.Grid[i.ToString() + j.ToString()].gameObject;
                        index++;
                    }
                }
                return true;
            }
        }

        return false;
    }

    public bool CheckGrid(int x, int y)
    {
        for (int i = x; i <= x + 2; i++)
        {
            for (int j = y; j <= y + 1; j++)
            {
                if (Inventory.instance.Grid[i.ToString() + j.ToString()].Taken)
                    return false;
            }
        }

        return true;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 2; i++)
        {
            if (CheckSlot("1" + i.ToString()))
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
        return false;
    }

    public override void ItemDropped()
    {
        InventoryImage.SetActive(true);
        image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        for (int i = 0; i <= 5; i++)
            Slots[i] = null;
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
        Destroy(gameObject);
        Debug.Log("Consume Stalagmite");
    }
}
