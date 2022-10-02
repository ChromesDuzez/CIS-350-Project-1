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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
      

      
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
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
               popUpIndex++;

            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerControllerScript.holdPoint.childCount == 0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        { //checks if player has pressed E and picked up something
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerControllerScript.holdPoint.childCount == 0)
            {
                throwCount++;
            }
            if (throwCount == 3)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            
        }
       
    }
}
