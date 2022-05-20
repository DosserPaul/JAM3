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

    void Start() {
        textDisplay.text = "" + format_time(minutes) + ":" + format_time(seconds) + ":" + format_time(miliSeconds);
    }

    void Update() {
        if (takingAway == false) {
            StartCoroutine(TimerTake());
        }
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

    // public Text textDisplay;
    // public int secondLeft = 30;
    // public bool takingAway = false;

    // void Start() {
    //     textDisplay.text = "00:" + secondLeft;
    // }

    // void Update() {
    //     if (takingAway == false && secondLeft > 0) {
    //         StartCoroutine(TimerTake());
    //     }
    // }

    // IEnumerator TimerTake()
    // {
    //     takingAway = true;
    //     yield return new WaitForSeconds(1);
    //     secondLeft -= 1;
    //     textDisplay.text = "00:" + secondLeft;
    //     takingAway = false;
    // }

    // private Text textClock;void Awake (){
    //     textClock = GetComponent<Text>();
    // }
  
    // void Update () {
    //     DateTime time = DateTime.MinValue;
    //     string minute = LeadingZero(time.Minute);
    //     string second = LeadingZero(time.Second);
    //     textClock.text = minute + ":" + minute;
    // }
  
    // string LeadingZero (int n) {
    //     return n.ToString().PadLeft(2, '0');
    // }
}
