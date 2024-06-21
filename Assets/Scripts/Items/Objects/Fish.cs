using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fish : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
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
        if (temp.x <= 0) //Left
            current = 1;
        else //Right
            current = 2;

        image.raycastTarget = false;
        foreach (GameObject slot in Slots)
            slot.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float divisor = GetDivisors() / 4;
        if (current == 1)
            transform.position = Input.mousePosition - new Vector3(-divisor, 0);
        else
            transform.position = Input.mousePosition - new Vector3(divisor, 0);
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
                y -= 1;

            if (y != 1 && y != 2)
                Debug.Log("Invalid");
            else if (CheckGrid(x, y))
            {
                Slots[0] = Inventory.instance.Grid[x.ToString() + y.ToString()].gameObject;
                Slots[1] = Inventory.instance.Grid[x.ToString() + (y + 1).ToString()].gameObject;
                return true;
            }
        }

        return false;
    }

    public bool CheckGrid(int x, int y)
    {
        if (Inventory.instance.Grid[x.ToString() + y.ToString()].Taken)
            return false;

        if (Inventory.instance.Grid[x.ToString() + (y + 1).ToString()].Taken)
            return false;

        return true;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 3; i++)
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
        if (player.sprintDuration <= 0)
            player.sprintDuration += speedDuration;
        else
            player.sprintDuration += speedDuration / 2;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Ate Fish");
        return true;
    }

    public override void ItemDropped(GameObject Character)
    {
        sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        Slots[0] = null;
        Slots[1] = null;
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
							pig.runSpeed = pig.runSpeed / 2;
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
        eatTime = 25;
        foodValue = 30;
        effect = "None";
        effectValue = 5;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Fish");
    }
}
