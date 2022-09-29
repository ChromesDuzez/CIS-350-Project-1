/*
* Devun Schneider
* CIS 350 - Trash Pick-Up Simulator
* This script manages the in-game pause menu
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject controlsMenu;
    private bool pauseOpen = false;
    private bool controlsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        controlsMenu = GameObject.FindGameObjectWithTag("ControlsMenu");
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if screen is not open, is set to active when player presses escape key and pauses the game
        if (!pauseOpen && Input.GetKeyDown(KeyCode.Escape))
        {

            //open pause menu
            pauseOpen = true;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
        }
        //if is open and player presses escape, hides pause menu and resumes game
        if (pauseOpen && Input.GetKeyDown(KeyCode.Escape))
        {

            //open pause menu
            pauseOpen = false;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
        }
        //controls escape button on controls menu
        if (controlsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            controlsMenu.SetActive(false);
        }
        
    }
    //manages Controls_Btn in PauseMenu
    public void openControlsMenu()
    {
        controlsOpen = true;
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false); //doesnt close pause menu and resume game
    }
    //manages Back_Btn in ControlsMenu
    public void closeControlsMenu()
    {
        controlsOpen = false;
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true); //doesnt close pause menu and resume game
    }

}
