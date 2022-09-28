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
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject finalPanel;

    public int count = 0;
    public bool[] arr = new bool[5];
    public int throwCount=0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        firstPanel = GameObject.Find("Panel_1"); firstPanel.SetActive(true);
        secondPanel = GameObject.Find("Panel_2"); secondPanel.SetActive(false);
        thirdPanel = GameObject.Find("Panel_3"); thirdPanel.SetActive(false);
        finalPanel = GameObject.Find("Panel_4"); finalPanel.SetActive(false);

        for(int i=0; i <= arr.Length; i++)
        {
            arr[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(firstPanel.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.W) && arr[0]==false)
            {
                count++;
                arr[0] = true;
            }
            if (Input.GetKeyDown(KeyCode.A) && arr[1] == false)
            {
                count++;
                arr[1] = true;
            }
            if (Input.GetKeyDown(KeyCode.S) && arr[2] == false)
            {
                count++;
                arr[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && arr[3] == false)
            {
                count++;
                arr[3] = true;
            }
        }
        if (count == 4)
        {
            //sets panel to active
            secondPanel.SetActive(true);
            //waits for player to press continue button (which makes the panel not active)
            if (secondPanel.activeSelf == false)
            {
                //checks if player has pressed E and picked up something
                if(Input.GetKeyDown(KeyCode.E) && playerControllerScript.holdPoint.childCount == 0)
                {
                    count++;
                }
            }
        }
        //sets the final panel active if both others are done
        if (count == 5)
        {
            thirdPanel.SetActive(true);
            if (thirdPanel.activeSelf == false)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0)&& playerControllerScript.holdPoint.childCount == 0)
                {
                    throwCount++;
                }
                if (throwCount == 0)
                {
                    finalPanel.SetActive(true);
                    count++;
                }
            }
        }
    }
}
