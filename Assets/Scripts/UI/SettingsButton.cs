using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image button;
    [SerializeField] private GameObject display;
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.color = new Color(249f / 256f, 239f / 256f, 255f / 256f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.color = new Color(56f / 256f, 42f / 256f, 61f / 256f);       
    }
}
