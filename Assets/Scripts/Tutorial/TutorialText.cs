using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private int numLines;
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    [SerializeField] private GameObject ClickAnywhere;
    private void Awake()
    {
        switch (numLines)
        {
            case 1:
                StartCoroutine(OneLine());
                break;
            case 2:
                StartCoroutine(TwoLine());
                break;
        }
    }

    private IEnumerator OneLine()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(3f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator TwoLine()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ClickAnywhere.SetActive(true);
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }
}
