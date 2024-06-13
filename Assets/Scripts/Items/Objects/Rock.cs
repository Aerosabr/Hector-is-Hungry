using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rock : Item
{
    [SerializeField] private GameObject BaseRock;
    public List<GameObject> Rocks = new List<GameObject>();

    public override void CheckSlot(string Pos)
    {
        
    }

    public void CheckSlot(string Location, int Pos)
    {

    }

    public override bool PickupItem()
    {
        throw new System.NotImplementedException();
    }

    public void BeginDrag()
    {
        foreach (GameObject rock in Rocks)
        {
            Rock temp = rock.GetComponent<Rock>();
            temp.parentAfterDrag = temp.gameObject.transform.parent;
            temp.transform.SetParent(temp.transform.root);
            temp.transform.SetAsLastSibling();
            temp.image.raycastTarget = false;
            temp.parentAfterDrag.gameObject.GetComponent<InventorySlot>().Taken = false;
        }
    }

    public void Dragging(int Pos)
    {
        switch (Pos)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-34, 34);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(34, 34);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(-34, -34);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(34, -34);
                break;
        }
    }
}
