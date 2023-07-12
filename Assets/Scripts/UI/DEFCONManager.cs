using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DEFCONManager : MonoBehaviour
{
    public float timeLeft;
    public bool timerRunning;
    int DEFCON;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI DEFCONText;

    void Start() 
    {
        timerRunning = true;
        DEFCON = 5;
    }

    void Update()
    {
        if(timerRunning)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                if(timeLeft <= 480) DEFCON = 4;
                if(timeLeft <= 360) DEFCON = 3;
                if(timeLeft <= 240) DEFCON = 2;
                if(timeLeft <= 120) DEFCON = 1;

                updateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerRunning = false;
                DEFCON = 0;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        DEFCONText.text = "DEFCON = " + DEFCON;;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
