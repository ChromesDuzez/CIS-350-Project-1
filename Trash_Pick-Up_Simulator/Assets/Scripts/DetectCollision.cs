/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the collision between the trash and the recepticals which incraments the score
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to the recepticals
public class DetectCollision : MonoBehaviour
{
    private ScoreManager displayScoreScript;
    //public HealthSystem healthSystem;
    //public bool getHealthOnKill;

    private void Start()
    {
        displayScoreScript = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
        //healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (getHealthOnKill) { healthSystem.AddHealth(); }
        displayScoreScript.incramentScore();
        Destroy(other.gameObject);
    }
}
