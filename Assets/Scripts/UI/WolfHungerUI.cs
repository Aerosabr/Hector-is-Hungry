using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WolfHungerUI : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    public static WolfHungerUI instance;

    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject InventoryImg;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private GameObject HighlightUI;
    [SerializeField] private int current;
    [SerializeField] private bool uiActive = true;
    [SerializeField] private Sprite icon1;
    [SerializeField] private Sprite icon2;

    public Wolf wolf;
    public Slider hungerBar;
    public Slider hungerBar2;
    public Transform Icon;
    public Transform Icon2;

    private void Start()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
		hungerBar.value = wolf.currentHunger / wolf.maxHunger;
		hungerBar2.value = wolf.currentHunger / wolf.maxHunger;
		float hungerRatio = wolf.currentHunger / wolf.maxHunger;
        if(hungerRatio > 0.3)
        {
			Icon.GetComponent<Image>().sprite = icon1;
			Icon2.GetComponent<Image>().sprite = icon1;
		}
        else if(hungerRatio < 0.3)
        {
			Icon.GetComponent<Image>().sprite = icon2;
			Icon2.GetComponent<Image>().sprite = icon2;
		}
		if (wolf.currentHunger > 0)
		{
			// Reduce the rotation speed and angle for a slower and less drastic movement
			float rotationSpeed = 5 * ((wolf.maxHunger - wolf.currentHunger) / wolf.maxHunger); // Reduced speed factor
			float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * (1 - hungerRatio) * 5f; // Reduced angle multiplier
			
			Icon.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
			Icon2.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
		}
		else if (wolf.currentHunger <= 0)
		{
			// Reduce the rotation speed and angle for a slower and less drastic movement
			float rotationSpeed = 15; // Reduced speed factor
			float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * 15f; // Reduced angle multiplier

           
			Icon.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
			Icon2.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
		}
	}

    private float GetDivisors()
    {
        Vector3[] corners = new Vector3[4];
        InventoryImg.GetComponent<RectTransform>().GetWorldCorners(corners);
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

        image.raycastTarget = false;
        Debug.Log("Began drag");
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
            image.raycastTarget = true;
            foreach (GameObject slot in Slots)
                slot.GetComponent<InventorySlot>().Taken = true;
        }
    }

    public override bool CheckSlot(string Pos)
    {
        if (!isDropped)
            foreach (GameObject slot in Slots)
                slot.GetComponent<InventorySlot>().Taken = false;
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
                sprite.enabled = false;
                icon.SetActive(false);
                UI.SetActive(false);
                InventoryImg.SetActive(true);
                box.enabled = false;
                uiActive = false;
                transform.localScale = Vector3.one;
                return true;
            }
        }

        return false;
    }

    public override bool EatItem(Player player)
    {
        return false;
    }

    public override void ItemDropped(GameObject Character)
    {
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
        isDropped = true;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        OnEndDrag(null);
        sprite.enabled = true;
        icon.SetActive(true);
        box.offset = Vector2.zero;
        box.size = new Vector2(2, 0.5f);
        InventoryImg.SetActive(false);
        image.raycastTarget = true;
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
        box.enabled = true;
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

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue, Transform wolf)
    {
        eatTime = 0;
        foodValue = 60;
        effect = "Stun";
        effectValue = 30;
		StartCoroutine(JumpIntoWolf(wolf));
		Debug.Log("Consume HungerBar");
	}

	private IEnumerator JumpIntoWolf(Transform wolf)
	{
		Vector3 startPosition = transform.position;
		Vector3 targetPosition = wolf.position;
		float duration = 0.5f;
		float elapsed = 0f;
		float arcHeight = 2f;

		box.enabled = false;
		box.excludeLayers |= LayerMask.GetMask("Character");

		while (elapsed < duration)
		{
			float t = elapsed / duration;
			Vector3 arcPosition = Vector3.Lerp(startPosition, targetPosition, t);
			arcPosition.y += Mathf.Sin(Mathf.PI * t) * arcHeight;

			transform.position = arcPosition;
			elapsed += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject, duration);
		transform.position = targetPosition;
		box.enabled = true;
	}
}

