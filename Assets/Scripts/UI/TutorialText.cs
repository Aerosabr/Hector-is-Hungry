using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public Animator ClickAnywhere;

    void Start()
    {
        StartCoroutine(Line2());
    }

    public IEnumerator Line2()
    {
        yield return new WaitForSeconds(1);
        anim1.Play("FadeIn");
        yield return new WaitForSeconds(2);
        anim2.Play("FadeIn");
        yield return new WaitForSeconds(2);
        ClickAnywhere.Play("FadeInAndOut");
    }
}
