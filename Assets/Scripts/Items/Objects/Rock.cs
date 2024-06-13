using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rock : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject TopLeft;
    public GameObject TopRight;
    public GameObject BottomLeft;
    public GameObject BottomRight;

    public int current;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        if (temp.x <= 0 && temp.y > 0) //Top left
            current = 1;
        else if (temp.x > 0 && temp.y > 0) //Top right
            current = 2;
        else if (temp.x <= 0 && temp.y <= 0) //Bottom left
            current = 3;
        else //Bottom right
            current = 4;

        image.raycastTarget = false;
        TopLeft.GetComponent<InventorySlot>().Taken = false;
        TopRight.GetComponent<InventorySlot>().Taken = false;
        BottomLeft.GetComponent<InventorySlot>().Taken = false;
        BottomRight.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-35, 35);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(35, 35);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(-35, -35);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(35, -35);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(((TopLeft.transform.position.x + BottomRight.transform.position.x) / 2), ((TopLeft.transform.position.y + BottomRight.transform.position.y) / 2));
        image.raycastTarget = true;
        TopLeft.GetComponent<InventorySlot>().Taken = true;
        TopRight.GetComponent<InventorySlot>().Taken = true;
        BottomLeft.GetComponent<InventorySlot>().Taken = true;
        BottomRight.GetComponent<InventorySlot>().Taken = true;
    }

    public override void CheckSlot(string Pos)
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
            }

            if (x == 0 || x == 4 || y == 0 || y == 4)
                Debug.Log("Invalid");
            else if (!Inventory.instance.Grid[x.ToString() + y.ToString()].Taken && !Inventory.instance.Grid[x.ToString() + (y+1).ToString()].Taken 
                && !Inventory.instance.Grid[(x+1).ToString() + y.ToString()].Taken && !Inventory.instance.Grid[(x+1).ToString() + (y+1).ToString()].Taken)
            {
                TopLeft = Inventory.instance.Grid[x.ToString() + y.ToString()].gameObject;
                TopRight = Inventory.instance.Grid[x.ToString() + (y+1).ToString()].gameObject;
                BottomLeft = Inventory.instance.Grid[(x+1).ToString() + y.ToString()].gameObject;
                BottomRight = Inventory.instance.Grid[(x+1).ToString() + (y+1).ToString()].gameObject;
            }
            
        }
    }

    public override bool PickupItem()
    {
        throw new System.NotImplementedException();
    }


}

