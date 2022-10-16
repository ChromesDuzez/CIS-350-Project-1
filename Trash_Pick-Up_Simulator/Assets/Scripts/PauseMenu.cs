/*
* Devun Schneider
* CIS 350 - Trash Pick-Up Simulator
* This script manages the in-game pause menu
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public bool pauseOpen = false;
    public bool controlsOpen = false;
    public Slider healthSlider;
    public PlayerController pc;
    public EnemyMove enemyMove;
    public Text ouchText;

    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        //controlsMenu = GameObject.FindGameObjectWithTag("ControlsMenu");
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ouchText = GameObject.FindGameObjectWithTag("ouch").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if screen is not open, is set to active when player presses escape key and pauses the game
        if (!pauseOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            //open pause menu
            pauseOpen = true;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        //if is open and player presses escape, hides pause menu and resumes game
        else if (pauseOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            //open pause menu
            pauseOpen = false;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        //controls escape button on controls menu
        else if (controlsOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            controlsMenu.SetActive(false);
        }

        healthSlider.value = pc.health;

        if(pc.isBeingHurt)
        {
            ouchText.text = "You are being attacked by a monster! Run!";
        }
        else
        {
            ouchText.text = "";
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
