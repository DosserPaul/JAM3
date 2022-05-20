using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock : MonoBehaviour {
    public Text textDisplay;
    public int seconds = 0;
    public int minutes = 0;
    public int miliSeconds = 0;
    public bool takingAway = false;
    bool isStoped = false;


    void Start() {
        textDisplay.text = "" + format_time(minutes) + ":" + format_time(seconds) + ":" + format_time(miliSeconds);
    }

    void Update() {
        if (takingAway == false && isStoped == false) {
            StartCoroutine(TimerTake());
        }
    }

    void Stop() {
        isStoped = true;
    }

    string format_time(int nb) {
        string tmp;
        if (nb < 10) {
            tmp = "0" + nb;
            return tmp;
        }
        tmp = "" + nb;
        return tmp;
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(0.001F);
        // yield return new WaitForSeconds(1);
        miliSeconds += 1;
        if (miliSeconds == 60) {
            seconds += 1;
            miliSeconds = 0;
        }
        if (seconds == 60) {
            minutes += 1;
            seconds = 0;
        }
        textDisplay.text = "" + format_time(minutes) + ":" + format_time(seconds) + ":" + format_time(miliSeconds);
        takingAway = false;
    }
}
