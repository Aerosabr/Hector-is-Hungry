using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Timer : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private GameObject InventoryImage;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Icon;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private GameObject HighlightUI;
    [SerializeField] private int current;
    [SerializeField] private Text textUI;
    [SerializeField] private Text textInventory;
    [SerializeField] private TextMeshPro textItem;
    [SerializeField] private bool uiActive = true;
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
        textInventory.text = minutes.ToString() + ":" + secondsString;
        textItem.text = minutes.ToString() + ":" + secondsString;
        textUI.text = minutes.ToString() + ":" + secondsString;
    }

    private float GetDivisors()
    {
        Vector3[] corners = new Vector3[4];
        InventoryImage.GetComponent<RectTransform>().GetWorldCorners(corners);
        return corners[2].x - corners[1].x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        float divisor = GetDivisors() / 6;
        if (temp.y >= 0) //Top half
        {
            if (temp.x <= -divisor)
                current = 1;
            else if (temp.x <= divisor)
                current = 2;
            else
                current = 3;
        }
        else //Bottom half
        {
            if (temp.x <= -divisor)
                current = 4;
            else if (temp.x <= divisor)
                current = 5;
            else
                current = 6;
        }

        InventoryImage.GetComponent<Image>().raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        float divisor = GetDivisors() / 6;
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-(2 * divisor), divisor);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(0, divisor);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(2 * divisor, divisor);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(-(2 * divisor), -divisor);
                break;
            case 5:
                transform.position = Input.mousePosition - new Vector3(0, -divisor);
                break;
            case 6:
                transform.position = Input.mousePosition - new Vector3(2 * divisor, -divisor);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {       
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[5].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[5].transform.position.y) / 2));
            InventoryImage.GetComponent<Image>().raycastTarget = true;
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
                    x -= 1;
                    break;
                case 5:
                    x -= 1;
                    y -= 1;
                    break;
                case 6:
                    x -= 1;
                    y -= 2;
                    break;
            }

            if ((x != 1 && x != 2) || y != 1)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Debug.Log("Valid");
                int index = 0;
                for (int i = x; i <= x + 1; i++)
                {
                    for (int j = 1; j <= 3; j++)
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
            for (int j = y; j <= y + 2; j++)
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
            if (CheckSlot(i.ToString() + "1"))
            {
                Debug.Log("Valid");
                isDropped = false;
                transform.SetParent(GameObject.Find("InventoryImages").transform);
                OnEndDrag(null);
                InventoryImage.SetActive(true);
                Icon.SetActive(false);
                UI.SetActive(false);
                box.enabled = false;
                uiActive = false;
                transform.localScale = Vector3.one;
                return true;
            }
        }

        return false;
    }

    public override void ItemDropped(GameObject Character)
    {
        isDropped = true;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        OnEndDrag(null);
        InventoryImage.SetActive(false);
        Icon.SetActive(true);
        box.enabled = true;
        InventoryImage.GetComponent<Image>().raycastTarget = true;
        for (int i = 0; i <= 5; i++)
            Slots[i] = null;
        current = 0;
        transform.localScale = Vector3.one;
		Transform character = Character.transform;
		transform.position = character.position;
		if (character.GetComponent<Rigidbody2D>().velocity.x > 0)
			StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(2f, 0f, 0f), 0.5f));
		else
			StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(-2f, 0f, 0f), 0.5f));
	}
	private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition, float duration)
	{
		Vector3 startPosition = transform.position;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			float height = 1f;
			Vector3 arcPosition = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
			arcPosition.y += Mathf.Sin(Mathf.Clamp01(elapsed / duration) * Mathf.PI) * height;

			Collider2D[] hits = Physics2D.OverlapCircleAll(arcPosition, 0.5f);
			foreach (Collider2D hit in hits)
			{
				if (hit.CompareTag("Wolf"))
					isMarked = true;
			}
			transform.position = arcPosition;
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.position = targetPosition;
	}
	public override void Highlight(bool toggle)
    {
        if (toggle)
        {
            if (uiActive)
                HighlightUI.SetActive(true);
            else
                HighlightObject.SetActive(true);
        }
        else
        {
            if (uiActive)
                HighlightUI.SetActive(false);
            else
                HighlightObject.SetActive(false);
        }
    }

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
    {
        eatTime = 75;
        foodValue = 10;
        effect = "Poison";
        effectValue = 5;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Stalagmite");
    }
}
