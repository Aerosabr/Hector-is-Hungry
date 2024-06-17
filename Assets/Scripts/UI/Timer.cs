using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Timer : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private GameObject InventoryImage;
    [SerializeField] private int current;
    public Text timerText;
    public float timerElapse = 0f;
    /*
    private void Awake()
    {
        RectTransform temp = GetComponent<RectTransform>();
        temp.anchorMin = new Vector2(0.5f, 0.5f);
        temp.anchorMax = new Vector2(0.5f, 0.5f);
        temp.pivot = new Vector2(0.5f, 0.5f);
        temp.anchoredPosition = Vector2.zero;
    }
    */
    private void Start()
    {
        StartCoroutine(startTimer());
    }

    private IEnumerator startTimer()
    {
        timerElapse = 0f;
        yield return null;
    }

    private void FixedUpdate()
    {
        timerElapse += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timerElapse / 60);
        int seconds = Mathf.FloorToInt(timerElapse % 60);
        string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        timerText.text = minutes.ToString() + ":" + secondsString;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        if (temp.x <= 0 && temp.y > 0) //Top left quadrant
        {
            if (temp.x <= -47.5)
                current = 1;
            else
                current = 2;
        }
        else if (temp.x > 0 && temp.y > 0) //Top right quadrant
        {
            if (temp.x <= 47.5)
                current = 3;
            else
                current = 4;
        }
        else if (temp.x <= 0 && temp.y <= 0) //Bottom left quadrant
        {
            if (temp.x <= -47.5)
                current = 5;
            else
                current = 6;
        }
        else //Bottom right quadrant
        {
            if (temp.x <= 47.5)
                current = 7;
            else
                current = 8;
        }

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-75, 25);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(-25, 25);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(25, 25);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(75, 25);
                break;
            case 5:
                transform.position = Input.mousePosition - new Vector3(-75, -25);
                break;
            case 6:
                transform.position = Input.mousePosition - new Vector3(-25, -25);
                break;
            case 7:
                transform.position = Input.mousePosition - new Vector3(25, -25);
                break;
            case 8:
                transform.position = Input.mousePosition - new Vector3(75, -25);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {       
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[7].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[7].transform.position.y) / 2));
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
                    y -= 2;
                    break;
                case 4:
                    y -= 3;
                    break;
                case 5:
                    x -= 1;
                    break;
                case 6:
                    x -= 1;
                    y -= 1;
                    break;
                case 7:
                    x -= 1;
                    y -= 2;
                    break;
                case 8:
                    x -= 1;
                    y -= 3;
                    break;
            }

            if (x <= 0 || x == 4 || y != 1)
                Debug.Log("Invalid");
            else if (!Inventory.instance.Grid[x.ToString() + y.ToString()].Taken && !Inventory.instance.Grid[x.ToString() + (y + 1).ToString()].Taken
                && !Inventory.instance.Grid[x.ToString() + (y + 2).ToString()].Taken && !Inventory.instance.Grid[x.ToString() + (y + 3).ToString()].Taken
                && !Inventory.instance.Grid[(x + 1).ToString() + y.ToString()].Taken && !Inventory.instance.Grid[(x + 1).ToString() + (y + 1).ToString()].Taken
                && !Inventory.instance.Grid[(x + 1).ToString() + (y + 2).ToString()].Taken && !Inventory.instance.Grid[(x + 1).ToString() + (y + 3).ToString()].Taken)
            {
                int index = 0;
                for (int i = x; i <= x + 1; i++)
                {
                    for (int j = 1; j <= 4; j++)
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

    public override bool PickupItem()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (CheckSlot(i.ToString() + "1"))
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
        for (int i = 0; i <= 7; i++)
            Slots[i] = null;
        current = 0;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        transform.position = GameObject.Find("Player").transform.position;
    }
    
}
