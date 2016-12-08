using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {

    public Text timeText;
    int minutes = 0;
    float timeCounter = 0, seconds = 0;
    bool startCount = false;
	
	void Update () {

        if (startCount) {
            timeCounter += Time.deltaTime;
            minutes = (int)(timeCounter / 60);
            seconds += Time.deltaTime;
            if (seconds >= 60)
                seconds = 0;
            string min = (minutes < 10) ? "0" + minutes.ToString() : minutes.ToString();
            string sec = (seconds < 10) ? "0" + ((int)seconds).ToString() : ((int)seconds).ToString();
            timeText.text = "Timer\n" + min + ":" + sec;
        }
    }

    public void StartTimer()
    {
        startCount = true;
    }
}
