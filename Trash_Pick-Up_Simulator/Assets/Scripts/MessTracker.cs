/*
* John Green and Zach Wilson
* CIS 350 - Group Project
* This script controls the mess tracker bar and the game over logic/text
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessTracker : MonoBehaviour
{
    public Slider messTracker;
    public TimerManager CountDownTimerScript;
    public int maxTrash = 50;
    public Text gameOverText;
    public bool gameOver = false;
    public bool hasLost = false;

    void Start()
    {
        Time.timeScale = 1;

        //gets the game over textbox if it does not initially get set
        if (gameOverText == null)
        {
            gameOverText = GameObject.FindWithTag("GameOverText").GetComponent<Text>() as Text;
        }
        if (CountDownTimerScript == null)
        {
            CountDownTimerScript = GameObject.FindWithTag("CountDownTimer").GetComponent<TimerManager>() as TimerManager;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (messTracker.value >= maxTrash && !gameOver)
        {
            Time.timeScale = 0;
            gameOverText.text = "Your world has been consumed...\nPress R to try again.";
            gameOver = true;
            hasLost = true;
        }

        if(gameOver && !hasLost)
        {
            Time.timeScale = 0;
            gameOverText.text = "Your world has been spared from the trash.\n(for now)\nPress R to play again.";
        }

        if(gameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
