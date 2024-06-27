using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInteraction : MonoBehaviour
{
    [SerializeField] private int Step;
    [SerializeField] private List<GameObject> Line = new List<GameObject>();
    [SerializeField] private List<GameObject> Images = new List<GameObject>();

    public bool Condition1;
    public bool Condition2;
    public bool Condition3;
    public bool Condition4;
    public bool Condition5;

    [SerializeField] private int numHaybales = 0;

    private void Awake()
    {
        Condition1 = false;
        Condition2 = false; 
        Condition3 = false;
        Condition4 = false;
        Condition5 = false;
        switch (Step)
        {
            case 3:
                StartCoroutine(Step3());
                break;
            case 4:
                StartCoroutine(Step4());
                break;
            case 6:
                StartCoroutine(Step6());
                break;
            case 8:
                StartCoroutine(Step8());
                break;
            case 10:
                StartCoroutine(Step10());
                break;
            case 11:
                StartCoroutine(Step11());
                break;
            case 13:
                StartCoroutine(Step13());
                break;
        }
    }

    private void Update()
    {
        if (Step == 3 && Condition5)
        {
            if (Input.GetKeyDown(KeyCode.W) && !Condition1)
            {
                Images[0].GetComponent<Image>().color = Color.green;
                Condition1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.A) && !Condition2)
            {
                Images[1].GetComponent<Image>().color = Color.green;
                Condition2 = true;
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Condition3)
            {
                Images[2].GetComponent<Image>().color = Color.green;
                Condition3 = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && !Condition4)
            {
                Images[3].GetComponent<Image>().color = Color.green;
                Condition4 = true;
            }

            if (Condition1 && Condition2 && Condition3 && Condition4)
                TutorialManager.instance.Step++;
        }
        else if (Step == 4 && Condition5)
        {
            if (Input.GetKeyDown(KeyCode.F) && !Condition1)
            {
                Images[0].GetComponent<Image>().color = Color.green;
                Condition1 = true;
            }
        }
        else if (Step == 8 && Condition5)
        {
            if (Images[8] == null)
            {
                Images[4].SetActive(true);
                Condition1 = true;
            }
            if (Images[9] == null)
            {
                Images[5].SetActive(true);
                Condition2 = true;
            }
            if (Images[10] == null)
            {
                Images[6].SetActive(true);
                Condition3 = true;
            }

            if (Condition1 && Condition2 && Condition3)
                TutorialManager.instance.Step++;
        }
        else if (Step == 10 && Condition5)
        {
            Line[2].GetComponent<Text>().text = "Rebuild: " + Images[4].GetComponent<House>().currentAmount + "/3";

            if (Images[4].GetComponent<House>().currentAmount == 3)
                TutorialManager.instance.Step++;
        }
        else if (Step == 11 && Condition5)
        {
            if (Images[1] == null)
                TutorialManager.instance.Step++;
        }
        else if (Step == 13 && Condition5)
        {
            if (Input.GetKeyDown(KeyCode.E) && !Condition1)
            {
                Images[0].GetComponent<Image>().color = Color.green;
                Condition1 = true;
            }

            if (Images[5] == null && !Condition2)
            {
                Condition2 = true;
                Images[3].SetActive(true);
            }

            if (Images[6] == null && !Condition3)
            {
                Condition3 = true;
                Images[4].SetActive(true);
            }

            if (Condition2 && Condition3)
                TutorialManager.instance.Step++;
        }
    }
    private IEnumerator Step3()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        Line[2].SetActive(true);
        foreach (GameObject image in Images)
        {
            image.SetActive(true);
            image.GetComponent<Animator>().Play("FadeIn");
        }
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }

    private IEnumerator Step4()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        Line[3].SetActive(true);
        Line[3].GetComponent<Animator>().Play("FadeIn");
        for (int i = 0; i <= 3; i++)
        {
            Images[i].SetActive(true);
            Images[i].GetComponent<Animator>().Play("FadeIn");
        }
        
        yield return new WaitForSeconds(.5f);
        Images[7].SetActive(true);
        Condition5 = true;
    }

    public void Step4TaskComplete()
    {
        if (!Condition2)
        {
            Condition2 = true;
            Images[4].SetActive(true);
            Images[8].SetActive(true);
        }
        else if (!Condition3)
        {
            Condition3 = true;
            Images[5].SetActive(true);
            Images[9].SetActive(true);
        }
        else if (!Condition4)
        {
            Condition4 = true;
            Images[6].SetActive(true);
            TutorialManager.instance.Step++;
        }
    }

    private IEnumerator Step6()
    {
        yield return new WaitForSeconds(.5f);
        Images[2].SetActive(false);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        Images[1].SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        Images[0].SetActive(true);
        Images[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }

    public void Step6TaskComplete()
    {
        TutorialManager.instance.Step++;
    }

    private IEnumerator Step8()
    {
        yield return new WaitForSeconds(.5f);
        Images[7].GetComponent<Image>().raycastTarget = false;
        Images[0].SetActive(true);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        for (int i = 1; i <= 3; i++)
        {
            Images[i].SetActive(true);
            Images[i].GetComponent<Animator>().Play("FadeIn");
        }
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }

    public void Step8TaskComplete(string item)
    {
        if (item == "Apple")
        {
            if (!Condition1)
            {
                Images[4].SetActive(true);
                Condition1 = true;
            }
            else
            {
                Images[5].SetActive(true);
                Condition2 = true;
            }
        }
        else if (item == "Sticks")
        {
            Condition3 = true;
            Images[6].SetActive(true);
        }
        
        if (Condition1 && Condition2 && Condition3)
            TutorialManager.instance.Step++;
    }

    private IEnumerator Step10()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Images[1].GetComponent<Image>().raycastTarget = true;
        Images[2].SetActive(true);
        Images[3].SetActive(true);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        Images[0].SetActive(true);
        Images[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }

    private IEnumerator Step11()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        Images[1].SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        Images[0].SetActive(true);
        Images[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }

    private IEnumerator Step13()
    {
        yield return new WaitForSeconds(.5f);
        Line[0].SetActive(true);
        Line[0].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(2.5f);
        Line[1].SetActive(true);
        Line[1].GetComponent<Animator>().Play("FadeIn");
        Images[5].SetActive(true);
        Images[6].SetActive(true);
        Images[7].GetComponent<Player>().canPickup = false;
        Images[7].GetComponent<Player>().canEat = true;
        yield return new WaitForSeconds(2.5f);
        Line[2].SetActive(true);
        Line[2].GetComponent<Animator>().Play("FadeIn");
        Images[0].SetActive(true);
        Images[0].GetComponent<Animator>().Play("FadeIn");
        Line[3].SetActive(true);
        Line[3].GetComponent<Animator>().Play("FadeIn");
        Images[1].SetActive(true);
        Images[1].GetComponent<Animator>().Play("FadeIn");
        Images[2].SetActive(true);
        Images[2].GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Condition5 = true;
    }
}
