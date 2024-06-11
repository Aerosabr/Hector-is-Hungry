using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timerElapse = 0f;

	private void Start()
	{
        StartCoroutine(startTimer());
	}
	public IEnumerator startTimer()
    {
        timerElapse = 0f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        timerElapse += Time.deltaTime;
		int minutes = Mathf.FloorToInt(timerElapse / 60);
		int seconds = Mathf.FloorToInt(timerElapse % 60);
		string secondsString = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
		timerText.text = minutes.ToString() + ":" + secondsString;
	}
}
