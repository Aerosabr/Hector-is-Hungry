using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GetTime : MonoBehaviour
{
    public Timer timer;
    public Text text;

    void Start()
    {
        if (timer == null)
        {
            transform.gameObject.SetActive(false);
            return;
        }
    }
}
