using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image button;
    [SerializeField] private GameObject display;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //button.color = Color.gray;
        display.transform.localScale = new Vector3(1.05f, 1.05f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // button.color = new Color(256, 256, 256, 1);
        display.transform.localScale = Vector3.one;
    }
}
