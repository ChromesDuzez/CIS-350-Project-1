/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the collision between the trash and the recepticals which incraments the score
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//attach to the recepticals
public class DetectCollision : MonoBehaviour
{
    private ScoreManager displayScoreScript;
    //public HealthSystem healthSystem;
    //public bool getHealthOnKill;

    private GameObject particles;

    public Slider messTracker;

    private void Start()
    {
        particles = gameObject.transform.GetChild(0).gameObject;
        particles.SetActive(false);
        displayScoreScript = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
        //healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (getHealthOnKill) { healthSystem.AddHealth(); }
        if (other.CompareTag("CanPickup"))
        {
            try
            {
               gameObject.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<GroundCollisionSFXMngr>().playTrashCollision(), GlobalSettings.volume);
            }
            catch
            {
                Debug.Log("Error trying to get the objects SFX Manager.");
            }
            StartCoroutine("ScoreParticles");
            displayScoreScript.incramentScore();
            Destroy(other.gameObject);
            messTracker.value--;
        }

    }

    IEnumerator ScoreParticles()
    {
        particles.SetActive(true);
        yield return new WaitForSeconds(2);
        particles.SetActive(false);
    }
}
