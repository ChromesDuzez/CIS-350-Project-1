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


public class TutorialManager : MonoBehaviour
{

    #region///////////// Public Variables: ///////////////

    public PlayerController playerControllerScript;
   

    public int popUpIndex = 0;
    public GameObject[] popups;
    public int throwCount=0;

    public GameObject spawner;
    public float timer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {


        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < popups.Length; i++)
        {
            if (i == popUpIndex)
            {
                popups[popUpIndex].SetActive(true);
            }
            else
            {
                popups[popUpIndex].SetActive(false);
            }
        }
        //movement panel showing
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;

            }
        }
        //pick up panel showing
        else if (popUpIndex == 1)
        {//checks if player has pressed E and picked up something
            //activates trash spawner
            spawner.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && playerControllerScript.holdPoint.childCount == 0)
            {
                popUpIndex++;
            }
        }
        //throwing panel showing
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerControllerScript.holdPoint.childCount == 0)
            {
                throwCount++;
            }
            if (throwCount == 3)
            {
                popUpIndex++;
                timer = 15;
            }
        }
        //environment instability panel
        else if (popUpIndex == 3)
        {
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            else
            {
                timer = 15;
                popUpIndex++;
            }
        }
        //environment monster panel
        else if (popUpIndex == 4)
        {
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
            }
            else
            {
                popUpIndex++;
            }
        }
        //congratulations panel
        else if (popUpIndex == 5)
        {
            Cursor.visible = true;
        }
      
       
    }
}
