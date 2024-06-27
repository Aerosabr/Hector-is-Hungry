using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] private List<GameObject> TutorialObjects = new List<GameObject>();
    [SerializeField] Wolf wolf;
    public GameObject ClickAnywhere;
    public int Step = 0;

    [SerializeField] private List<GameObject> TutorialEnd = new List<GameObject>();
    private bool TutorialEnded;
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

        if (Step < 6)
            wolf.currentHunger = 100;
        else if (wolf.currentHunger < 2)
            wolf.currentHunger = 2;
        
        if (Step == 14 && !TutorialEnded)
            EndTutorial();
    }

    public void ClickAnywhereButton()
    {
        Step++;
        ClickAnywhere.SetActive(false);
    }

    public void EndTutorial()
    {
        Debug.Log("Tutorial Ending");
        Destroy(TutorialEnd[0]);
        Destroy(TutorialEnd[1]);
        Destroy(TutorialEnd[2]);

        TutorialEnd[3].GetComponent<RegionManager>().LoadRegions();
        StartCoroutine(TutorialEnd[3].GetComponent<RegionManager>().SpawnClouds());

        TutorialEnd[4].GetComponent<Player>().canEat = true;
        TutorialEnd[4].GetComponent<Player>().canPickup = true;

        Destroy(TutorialEnd[5]);

        TutorialEnd[6].SetActive(true);
        TutorialEnd[7].SetActive(true);

        TutorialEnd[9].GetComponent<CircleCollider2D>().enabled = true;

        StartCoroutine(WolfStats());
        TutorialEnded = true;
    }

    private IEnumerator WolfStats()
    {
        yield return new WaitForSeconds(5f);
        TutorialEnd[8].GetComponent<Wolf>().currentHunger = 100;
        TutorialEnd[8].GetComponent<Wolf>().eatingSpeed = 2f;
        Destroy(gameObject);
    }
}
