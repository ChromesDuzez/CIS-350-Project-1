/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the timer and flips the game over boolean if the timer hits 0
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float startingMinutes;
    public float startingSeconds;
    public MessTracker messTrackerScript;
    public float timeValue;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        if(startingMinutes == 0 && startingSeconds == 0)
        {
            startingMinutes = 8;
            startingSeconds = 30;
        }
        timeValue = (startingMinutes * 60) + startingSeconds;


        //gets the script if not pre-set
        if (messTrackerScript == null)
        {
            messTrackerScript = GameObject.FindWithTag("MessTracker").GetComponent<MessTracker>() as MessTracker;
        }

        //gets the textbox if it is not pre-set [not working yet idk why too tired to figure it out]
        if (timeText == null)
        {
            timeText = GameObject.FindWithTag("CountDownTimer").GetComponent<Text>() as Text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            messTrackerScript.gameOver = true;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Time Remaining:\n{0:00}:{1:00}", minutes, seconds);
    }
}
