using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] private List<GameObject> TutorialObjects = new List<GameObject>();
    public GameObject ClickAnywhere;
    public int Step = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < TutorialObjects.Count; i++)
        {
            if (i == Step)
                TutorialObjects[i].SetActive(true);
            else
                TutorialObjects[i].SetActive(false);
        }
    }

    public void ClickAnywhereButton()
    {
        Step++;
        ClickAnywhere.SetActive(false);
    }
}
