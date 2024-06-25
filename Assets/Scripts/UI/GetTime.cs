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
        else
        {
            int minutes = Mathf.FloorToInt(timer.timerElapse / 60);
            int seconds = Mathf.FloorToInt(timer.timerElapse % 60);
            string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
            text.text = "Time: " + minutes.ToString() + ":" + secondsString;
        }
    }
}
