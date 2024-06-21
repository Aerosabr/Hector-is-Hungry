using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Meteor : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject HighlightObject;
    [SerializeField] private int current;

    private float GetDivisors()
    {
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        return corners[1].y - corners[0].y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition - transform.position;
        float divisor = GetDivisors() / 6;
        if (temp.y >= divisor) //Top third
        {
            if (temp.x <= -divisor)
                current = 1;
            else if (temp.x <= divisor)
                current = 2;
            else
                current = 3;
        }
        else if (temp.y >= -divisor) //Middle third
        {
            if (temp.x <= -divisor)
                current = 4;
            else if (temp.x <= divisor)
                current = 5;
            else
                current = 6;
        }
        else //Bottom third
        {
            if (temp.x <= -divisor)
                current = 7;
            else if (temp.x <= divisor)
                current = 8;
            else
                current = 9;
        }

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        float divisor = GetDivisors() / 3;
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(-divisor, divisor);
                break;
            case 2:
                transform.position = Input.mousePosition - new Vector3(0, divisor);
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(divisor, divisor);
                break;
            case 4:
                transform.position = Input.mousePosition - new Vector3(-divisor, 0);
                break;
            case 5:
                transform.position = Input.mousePosition;
                break;
            case 6:
                transform.position = Input.mousePosition - new Vector3(divisor, 0);
                break;
            case 7:
                transform.position = Input.mousePosition - new Vector3(-divisor, -divisor);
                break;
            case 8:
                transform.position = Input.mousePosition - new Vector3(0, -divisor);
                break;
            case 9:
                transform.position = Input.mousePosition - new Vector3(divisor, -divisor);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[8].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[8].transform.position.y) / 2));
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
                case 7:
                    x -= 2;
                    break;
                case 8:
                    x -= 2;
                    y -= 1;
                    break;
                case 9:
                    x -= 2;
                    y -= 2;
                    break;
            }
            if (x != 1 || y != 1)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                int index = 0;
                for (int i = x; i <= x + 2; i++)
                {
                    for (int j = y; j <= y + 2; j++)
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
            for (int j = 1; j <= 2; j++)
            {
                if (CheckSlot(i.ToString() + j.ToString()))
                {
                    isDropped = false;
                    isMarked = false;
                    transform.SetParent(GameObject.Find("InventoryImages").transform);
                    OnEndDrag(null);
					sprite.enabled = false;
					image.enabled = true;
                    box.enabled = false;
                    transform.localScale = new Vector3(1, 1, 1);
                    return true;
                }
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
		sprite.enabled = true;
		image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        for (int i = 0; i <= 5; i++)
            Slots[i] = null;
        current = 0;
        transform.SetParent(GameObject.Find("RegionManager").transform);
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
            HighlightObject.SetActive(true);
        else
            HighlightObject.SetActive(false);
    }

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
    {
        eatTime = 150;
        foodValue = 75;
        effect = "None";
        effectValue = 5;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Meteor");
    }
}
