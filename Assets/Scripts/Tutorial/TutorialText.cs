using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private int Step;
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    [SerializeField] private GameObject ClickAnywhere;
    [SerializeField] private GameObject step5Arrow;
    [SerializeField] private GameObject HouseHay;
    [SerializeField] private GameObject PigHay;

    private void Awake()
    {
        switch (Step)
        {
            case 1:
                StartCoroutine(Line1());
                break;
            case 2:
                StartCoroutine(Line2());
                break;
            case 3:
                StartCoroutine(Line3());
                break;
            case 5:
                StartCoroutine(Line5());
                break;
            case 7:
                StartCoroutine(Line7());
                break;
            case 9:
                StartCoroutine(Line9());
                break;
            case 12:
                StartCoroutine(Line12());
                break;
            case 14:
                StartCoroutine(Line14());
                break;
        }
    }

    private IEnumerator Line1()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line2()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line3()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line5()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        step5Arrow.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line7()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        step5Arrow.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line9()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        HouseHay.SetActive(true);
        PigHay.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line12()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        HouseHay.SetActive(true);
        PigHay.SetActive(true);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }

    private IEnumerator Line14()
    {
        yield return new WaitForSeconds(.5f);
        line1.SetActive(true);
        line1.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        line2.SetActive(true);
        line2.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2f);
        ClickAnywhere.SetActive(true);
        ClickAnywhere.GetComponent<Animator>().Play("FadeInAndOut");
        TutorialManager.instance.ClickAnywhere.SetActive(true);
    }
}
