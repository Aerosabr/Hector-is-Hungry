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
                    isMarked = false;
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

    public override bool EatItem(Player player)
    {
        if (player.sprintDuration <= 0)
            player.sprintDuration += speedDuration;
        else
            player.sprintDuration += speedDuration / 2;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Ate Apple");
        return true;
    }

    public override void ItemDropped(GameObject Character)
    {
		sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        isDropped = true;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        Transform character = Character.transform;
        transform.position = character.position;
        transform.localScale = new Vector3(.15f, .15f, .15f);
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
                    if(hit.TryGetComponent(out Pig pig) && character.tag == "Player")
                    {
                        if(pig.item == null)
                        {
							pig.item = transform.gameObject;
                            pig.runSpeed = pig.runSpeed / 1;
							isMarked = false;
							transform.SetParent(pig.transform);
							sprite.enabled = false;
							box.enabled = false;
                            box.excludeLayers |= LayerMask.GetMask("Character");
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
        eatTime = 1;
        foodValue = 10;
        effect = "None";
        effectValue = 0;
        region.numActive--;
        Destroy(gameObject);
        Debug.Log("Consume Apple");
    }
}
