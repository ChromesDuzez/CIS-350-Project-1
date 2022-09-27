using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessTracker : MonoBehaviour
{
    public Slider messTracker;
    public int maxTrash = 50;
    public Text lossText;
    public bool hasLost = false;

    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (messTracker.value >= maxTrash)
        {
            Time.timeScale = 0;
            lossText.text = "Your world has been consumed...\nPress R to try again.";
            hasLost = true;
        }

        if(hasLost)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
