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

    public int popUpIndex = 0;
    public GameObject[] popups;
   
    public float timer=4;
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

        //look around panel showing
        if (popUpIndex == 0)
        {

            //counts down on the second timer
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //once the timer is 0, reset the timer and go to next panel
            else
            {
                popUpIndex++;
                timer = 4;
            }

        }
       
        //movement panel showing
        else if (popUpIndex == 1)
        {
            
            //once they press a key go to next panel
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;

            }
        }
        //jump panel showing
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        //Sprint panel showing
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                popUpIndex++;
            }
        }
        
        //pick up panel showing
        else if (popUpIndex == 4)
        {

            // checks if player has picked up something, if so goes to next panel
            if (playerControllerScript.holdPoint.childCount != 0)
            {
                popUpIndex++;
            }


        }
        //throwing panel showing
        else if (popUpIndex == 5)
        {
            //checks if they press LMB and is holding a piece of trash
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerControllerScript.holdPoint.childCount != 0)
            {
                popUpIndex++;
               
                timer = 4;
            }
        }
        //environment instability panel
        else if (popUpIndex == 6)
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
                timer = 5;
            }
        }
        //environment monster panel
        else if (popUpIndex == 7)
        {
            //counts down on the 10 second timer
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //once the timer is 0, go to next panel
            else 
            {
                timer = 7;
                popUpIndex++;
            }
        }
        //congratulations panel
        else if (popUpIndex == 8)
        {
            //counts down on the 10 second timer
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //once the timer is 0, go to next panel
            else
            {
                continueClicked = true;
            }


            //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            //{
            //    continueClicked = true;
            //    starts the timer
            //      timer = timer - Time.deltaTime;
            //
            //     messSlider.messTracker.value = 0f;
            //}
        }

    }
}
