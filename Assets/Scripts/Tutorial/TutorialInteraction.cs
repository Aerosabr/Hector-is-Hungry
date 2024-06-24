using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    [SerializeField] private int Step;
    [SerializeField] private List<GameObject> Line = new List<GameObject>();
    [SerializeField] private List<GameObject> Images = new List<GameObject>();

    private void Awake()
    {
        switch (Step)
        {
            case 1:
                StartCoroutine(Step3());
                break;
            case 2:
                StartCoroutine(Step6());
                break;
        }
    }

    private IEnumerator Step3()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
    }

    private IEnumerator Step6()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
