/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the ground collision Audio Effects that can be assigned to any throwable object
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionSFXMngr : MonoBehaviour
{
    //variables
    public bool isOnGround = true;

    //scripts
    public MessTracker messTrackerScript;
    public bool canGetTrackerScript;
    public AssignedEffectsManager SFXScript;
    private AudioSource goAS;
    

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        //gets the messTrackerScript if it is not pre-set
        try
        {
            if (messTrackerScript == null)
            {
                messTrackerScript = GameObject.FindWithTag("MessTracker").GetComponent<MessTracker>() as MessTracker;
            }
            canGetTrackerScript = true;
        }
        catch
        {
            Debug.Log("Can't Find the MessTracker Script!");
            canGetTrackerScript = false;
        }
        //gets the AssignedEffectsManager script if it is not pre-set
        try
        {
            SFXScript = GetComponent<AssignedEffectsManager>();
        }
        catch
        {
            Debug.Log("Can't Find the AssignedEffectsManager Script!");
            canGetTrackerScript = false;
        }
        //gets the audiosource
        try
        {
            goAS = GetComponent<AudioSource>();
        }
        catch
        {
            Debug.Log("Couldn't get the audio source! This shouldn't be possible. Key Word: shouldn't");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(canGetTrackerScript)
        {
            if (!collision.gameObject.CompareTag("TrashCan") && !messTrackerScript.gameOver && (SFXScript.collisionGround.Length > 0) && !isOnGround)
            {
                isOnGround = true;
                //Play sound effect
                try
                {
                    int hitGroundSFXIndex = Random.Range(0, SFXScript.collisionGround.Length);
                    goAS.PlayOneShot(SFXScript.collisionGround[hitGroundSFXIndex], GlobalSettings.volume);
                    Debug.Log("Played: " + SFXScript.collisionGround[hitGroundSFXIndex] + " at volume " + GlobalSettings.volume);
                }
                catch
                {
                    Debug.Log("Error trying to play sound.");
                }
            }
        }
    }
}
