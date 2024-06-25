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
        Condition5 = true;
    }

    public void Step4TaskComplete()
    {
        if (!Condition2)
        {

        }
        else if (!Condition3)
        {

        }
        else if (!Condition4)
        {

        }
    }
}
