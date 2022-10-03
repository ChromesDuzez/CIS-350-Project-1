/*****************************************************************************
// File Name :         TutorialManager.cs
// Author :            Devun Schneider
// Creation Date :     September 26, 2022
//
// Brief Description : A C# script that handles the player tutorial panels
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{

    #region///////////// Public Variables: ///////////////

    public PlayerController playerControllerScript;

    public Text countThrow;

    public int popUpIndex = 0;
    public GameObject[] popups;
    public int throwCount=0;

   
    public float timer=7;
    private bool continueClicked = false;
    private bool done = false;

   // public MessTracker messSlider;
    #endregion


    // Update is called once per frame
    void Update()
    {
        if (!continueClicked && !done)
        {
            for (int i = 0; i < popups.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popups[i].SetActive(true);
                }
                else
                {
                    popups[i].SetActive(false);
                }
            }
            countThrow.text = "After you pick up trash, Press and Hold Left Mouse to Throw" +
                     " Trash into a Trash Can, the longer you hold down LMB, the farther you throw! The distance" +
                     " is based on the power bar in the bottom left corner. Try throwing 3 things:" + throwCount + "/3 thrown.";
        }
        else if(continueClicked)
        {
            for (int i = 0; i < popups.Length; i++)
            {
                popups[i].SetActive(false);
            }
            done = true;
        }
   
       
        //goes through a loop to set the current panel active and all others inactive
        //from https://www.youtube.com/watch?v=a1RFxtuTVsk&ab_channel=Blackthornprod
       
        //movement panel showing
        if (popUpIndex == 0)
        {
            
            //once they press all 4 keys go to next panel
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;

            }
        }
        //pick up panel showing
        else if (popUpIndex == 1)
        {

            // checks if player has picked up something, if so goes to next panel
            if (playerControllerScript.holdPoint.childCount != 0)
            {
                popUpIndex++;
            }


        }
        //throwing panel showing
        else if (popUpIndex == 2)
        {
            //checks if they press LMB and is holding a piece of trash
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerControllerScript.holdPoint.childCount != 0)
            {
                
                throwCount++;
            }
            //if they have thrown 3 things then go to next panel and set the timer
            if (throwCount == 3)
            {
                popUpIndex++;
                timer = 7;
            }
        }
        //environment instability panel
        else if (popUpIndex == 3)
        {
            
            //counts down on the 10 second timer
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //once the timer is 0, reset the timer and go to next panel
            else
            {
                popUpIndex++;
                timer = 7;
            }
        }
        //environment monster panel
        else if (popUpIndex == 4)
        {
            //counts down on the 10 second timer
            if (timer > 0)
            {
               
            }
            //once the timer is 0, go to next panel
            else 
            {
                timer = 7;
                popUpIndex++;
            }
        }
        //congratulations panel
        else if (popUpIndex == 5)
        {
            
            
            if (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                continueClicked = true;
                //starts the timer
                timer = timer - Time.deltaTime;

               // messSlider.messTracker.value = 0f;
            }
        }

    }
}
