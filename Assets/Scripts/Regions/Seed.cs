using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seed : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private GameObject InventoryImage;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private int current;
    [SerializeField] private GameObject item;
    [SerializeField] private bool Spawnable = true;

    public bool isSeed = true;
    

    private void FixedUpdate()
    {
        if (isSeed)
        {
            if (Spawnable && region.numActive < 6)
                StartCoroutine(SpawnItem());

            bool temp = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Item" && collider.name != "Seed(Clone)")
                {
                    box.enabled = false;
                    temp = true;
                    break;
                }
            }

            if (!temp)
                box.enabled = true;
        }
    }

    #region Seed Functions
    private IEnumerator SpawnItem()
    {
        float spawnDur = Random.Range(item.GetComponent<Item>().spawnDuration / 2, item.GetComponent<Item>().spawnDuration);
        yield return new WaitForSeconds(spawnDur);
        if (region.numActive < 6)
            Spawning(item);
        Spawnable = true;
    }

    private void Spawning(GameObject item)
    {
        GameObject temp = Instantiate(item, region.transform);
        temp.transform.position = transform.position - new Vector3(0.5f, 0.5f);
        temp.GetComponent<Item>().region = region;
        region.numActive++;
    }

    public void InitiateSeed(Vector3 location, Region region)
    {
        this.region = region;
        transform.localPosition = location;
    }
    #endregion

    #region Item Functions
    private float GetDivisors()
    {
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        return corners[2].x - corners[1].x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        if (temp.y >= 0) //Top half
        {
            if (temp.x <= 0)
                current = 1;
            else
                current = 2;
        }
        else //Bottom half
        {
            if (temp.x <= 0)
                current = 3;
            else
                current = 4;
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
                transform.position = Input.mousePosition - new Vector3(-divisor, divisor);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(divisor, divisor);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(-divisor, -divisor);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(divisor, -divisor);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[3].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[3].transform.position.y) / 2));
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
            }

            if ((x != 1 && x != 2) || (y != 1 && y != 2))
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Debug.Log("Valid");
                int index = 0;
                for (int i = x; i <= x + 1; i++)
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
        for (int i = x; i <= x + 1; i++)
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
            for (int j = 1; j <= 2; j++)
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
                    isSeed = false;
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
        for (int i = 0; i <= 3; i++)
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
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Seed");
    }
    #endregion  
}
