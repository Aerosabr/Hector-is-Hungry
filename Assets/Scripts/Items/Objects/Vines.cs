using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Vines : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private List<GameObject> Slots = new List<GameObject>();
    [SerializeField] private SpriteRenderer sprite;
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
        float divisor = GetDivisors() / 2;
        if (temp.y >= divisor) //Top
            current = 1;
        else if (temp.y >= -divisor) //Middle
            current = 2;
        else //Bottom
            current = 3;

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float divisor = GetDivisors();
        switch (current)
        {
            case 1:
                transform.position = Input.mousePosition - new Vector3(0, divisor);
                break;
            case 2:
                transform.position = Input.mousePosition;
                break;
            case 3:
                transform.position = Input.mousePosition - new Vector3(0, -divisor);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.position = new Vector3(((Slots[0].transform.position.x + Slots[2].transform.position.x) / 2), ((Slots[0].transform.position.y + Slots[2].transform.position.y) / 2));
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
                    x -= 1;
                    break;
                case 3:
                    x -= 2;
                    break;
            }

            if (x != 1)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Debug.Log("Valid");
                int index = 0;
                for (int i = x; i <= x + 2; i++)
                {
                    Slots[index] = Inventory.instance.Grid[i.ToString() + y.ToString()].gameObject;
                    index++;
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
            if (Inventory.instance.Grid[i.ToString() + y.ToString()].Taken)
                return false;
        }

        return true;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (CheckSlot("1" + i.ToString()))
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
        return false;
    }

    public override bool EatItem(Player player)
    {
        if (player.sprintDuration <= 0)
            player.sprintDuration += speedDuration;
        else
            player.sprintDuration += speedDuration / 2;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Ate Vines");
        return true;
    }

    public override void ItemDropped(GameObject Character)
    {
        sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        isDropped = true;
        for (int i = 0; i <= 2; i++)
            Slots[i] = null;
        current = 0;
		transform.SetParent(GameObject.Find("RegionManager").transform);
		transform.localScale = Vector3.one;
        Transform character = Character.transform;
		transform.position = character.position;
		if (character.tag == "Player")
		{
			if (character.GetComponent<Rigidbody2D>().velocity.x > 0)
				StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(2f, 0f, 0f), 0.5f, character));
			else
				StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(-2f, 0f, 0f), 0.5f, character));
		}
		else
		{
			if (character.GetComponent<Rigidbody2D>().velocity.x > 0)
				StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(3f, 0f, 0f), 0.5f, character));
			else
				StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(-3f, 0f, 0f), 0.5f, character));
		}
	}
	private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition, float duration, Transform character)
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
				else if (hit.CompareTag("NPC"))
				{
					if (hit.TryGetComponent(out Pig pig) && character.tag == "Player")
					{
						if (pig.item == null)
						{
							pig.item = transform.gameObject;
							pig.runSpeed = pig.runSpeed / 3;
							isDropped = false;
							isMarked = false;
							transform.SetParent(pig.transform);
							sprite.enabled = false;
							box.enabled = false;
							transform.localScale = new Vector3(1, 1, 1);
						}
					}
				}
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
            HighlightObject.SetActive(true);
        else
            HighlightObject.SetActive(false);
    }

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue)
    {
        eatTime = Random.Range(4f, 6f);
        foodValue = 30;
        effect = "None";
        effectValue = 0;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Vines");
    }
}
