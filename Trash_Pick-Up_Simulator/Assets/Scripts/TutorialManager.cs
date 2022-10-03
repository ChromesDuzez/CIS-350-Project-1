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

    public GameObject spawner;
    public float timer=10;
  
    #endregion
    // Start is called before the first frame update
    void Start()
    {
       

        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        //goes through a loop to set the current panel active and all others inactive
        //from https://www.youtube.com/watch?v=a1RFxtuTVsk&ab_channel=Blackthornprod
        for (int i=0; i < popups.Length; i++)
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
        //movement panel showing
        if (popUpIndex == 0)
        {
            spawner.SetActive(false);
            //once they press all 4 keys go to next panel
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                //counts down on the 10 second timer
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                //once the timer is 0, reset the timer and go to next panel
                else
                {
                    timer = 10;
                    popUpIndex++;
                }

            }
        }
        //pick up panel showing
        else if (popUpIndex == 1)
        {
            //activates trash spawner
            spawner.SetActive(true);
            // checks if player has picked up something, if so goes to next panel
            if (playerControllerScript.holdPoint.childCount == 0)
            {
                //counts down on the 10 second timer
                if (timer > 0)
                {
                    timer = timer - Time.deltaTime;
                }
                //once the timer is 0, reset the timer and go to next panel
                else
                {
                    timer = 10;
                    popUpIndex++;
                }

            }
        }
        //throwing panel showing
        else if (popUpIndex == 2)
        {
            //checks if they press LMB and is holding a piece of trash
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerControllerScript.holdPoint.childCount == 0)
            {
                countThrow.text = "After you pick up trash, Press and Hold Left Mouse to Throw" +
                    " Trash into a Trash Can, the longer you hold down LMB, the farther you throw! The distance" +
                    " is based on the power bar in the bottom left corner. Try throwing 3 things:" + throwCount + "/3 thrown.";
                throwCount++;
            }
            //if they have thrown 3 things then go to next panel and set the timer to 15 seconds
            if (throwCount == 3)
            {
                popUpIndex++;
                timer = 10;
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
                timer = 10;
                popUpIndex++;
            }
        }
        //environment monster panel
        else if (popUpIndex == 4)
        {
            //counts down on the 10 second timer
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            //once the timer is 0, go to next panel
            else
            {
                popUpIndex++;
            }
        }
        //congratulations panel
        else if (popUpIndex == 5)
        {
            //shows cursor to press continue button
            Cursor.visible = true;
        }
      
       
    }
}
