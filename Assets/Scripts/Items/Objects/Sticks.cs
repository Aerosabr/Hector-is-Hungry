using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sticks : Item, IBeginDragHandler, IEndDragHandler, IDragHandler, IConsumable
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
        if (!isDropped)
            foreach (GameObject slot in Slots)
                slot.GetComponent<InventorySlot>().Taken = false;
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
					MusicManager.instance.soundSources[17].Play();
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
        
        MusicManager.instance.soundSources[16].Play();
		sprite.enabled = true;
        image.raycastTarget = true;
        image.enabled = false;
        isDropped = true;
        
        current = 0;
        transform.SetParent(GameObject.Find("RegionManager").transform);
        transform.localScale = Vector3.one;
        Transform character = Character.transform;
		transform.position = character.position;
		if (character.tag == "Player")
		{
			foreach (GameObject slot in Slots)
				slot.GetComponent<InventorySlot>().Taken = false;
			Slots[0] = null;
			Slots[1] = null;
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
						if (pig.item == null && pig.canHelp)
						{
							MusicManager.instance.soundSources[17].Play();
							pig.item = transform.gameObject;
							pig.runSpeed = pig.runSpeed / 2;
							isMarked = false;
							transform.SetParent(pig.transform);
							sprite.enabled = false;
							box.enabled = false;
                            box.excludeLayers |= LayerMask.GetMask("Character");
                            transform.localScale = new Vector3(1, 1, 1);
						}
					}
				}
				else if (hit.CompareTag("NPC"))
				{
					if (hit.TryGetComponent(out House house) && character.tag == "Player")
					{
						MusicManager.instance.soundSources[17].Play();
						Destroy(hit.gameObject, 1f);
						yield return null;
					}
				}
				else if (hit.CompareTag("Building"))
				{
					if (hit.TryGetComponent(out House house) && character.tag == "Player")
					{
						if (house.AddMaterial(this.transform))
						{
							MusicManager.instance.soundSources[17].Play();
							Destroy(transform.gameObject, 1f);
							yield return null;
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

    public void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue, Transform wolf)
    {
        eatTime = Random.Range(2f, 3f);
        foodValue = 20;
        effect = "None";
        effectValue = 0;
        region.numActive--;
		StartCoroutine(JumpIntoWolf(wolf));
		Debug.Log("Consume Stick");
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
