using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Apple : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform parentAfterDrag;
    [SerializeField] private GameObject HighlightObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        parentAfterDrag.gameObject.GetComponent<InventorySlot>().Taken = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDropped)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            parentAfterDrag.GetComponent<InventorySlot>().Taken = true;
        }
    }

    public override bool CheckSlot(string Pos)
    {
        if (!Inventory.instance.Grid[Pos].Taken)
        {
            parentAfterDrag = Inventory.instance.Grid[Pos].gameObject.transform;
            return true;
        }

        return false;
    }

    public override bool PickupItem()
    {
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                if (!Inventory.instance.Grid[i.ToString() + j.ToString()].Taken)
                {
                    isDropped = false;
                    InventorySlot openSlot = Inventory.instance.Grid[i.ToString() + j.ToString()];
                    transform.SetParent(openSlot.transform);
                    openSlot.Taken = true;
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

    public override void ItemDropped()
    {
        Transform player = GameObject.Find("Player").transform;
		sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        box.enabled = true;
        isDropped = true;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        transform.position = player.position;
        transform.localScale = new Vector3(.15f, .15f, .15f);
        if(player.GetComponent<Rigidbody2D>().velocity.x > 0)
		    StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(3f, 0f, 0f), 0.5f));
        else
			StartCoroutine(MoveToPositionCoroutine(transform.localPosition + new Vector3(-3f, 0f, 0f), 0.5f));
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

			transform.position = arcPosition;
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.position = targetPosition; // Ensure it reaches exactly the target position
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
        eatTime = 50;
        foodValue = 15;
        effect = "None";
        effectValue = 0;
        Destroy(gameObject);
        Debug.Log("Consume Apple");
    }
}
