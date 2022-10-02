/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the settings for Audio during the game
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class globals
{
    public static float volume = 1.0f; //will need to implement a way to change this setting and any other subsequent audio settings but for now we just have mute/unmute
    public static bool bUnMuted = true;
}

public class AudioSettingsManager : MonoBehaviour
{
    Toggle muteToggle;
    public Text mText;
    public Image mutedIconSprite;
    bool mutedIconSpriteSet;
    public bool unMutedBoolDebug = globals.bUnMuted;

    // Start is called before the first frame update
    void Start()
    {
        //get the toggle object
        muteToggle = GameObject.FindWithTag("MuteToggle").GetComponent<Toggle>() as Toggle;
        //start the listener for a value change
        muteToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(muteToggle); 
        });
        //get the muted sprite (or isOn == false sprite that has to be managed by this script)
        if (mutedIconSprite == null)
        {
            try
            {
                mutedIconSprite = GameObject.FindWithTag("MutedSprite").GetComponent<Image>() as Image;
                mutedIconSpriteSet = true;
            }
            catch
            {
                Debug.Log("No Sound Muted Icon Sprite Found!");
                mutedIconSpriteSet = false;
            }
        }
        else
        {
            mutedIconSpriteSet = true;
        }

        //start the muteToggle isOn variable at what the global variable is set to
        muteToggle.isOn = globals.bUnMuted;

        //update the properties of the toggle appropriately
        ToggleValueChanged(muteToggle);
    }

    void ToggleValueChanged(Toggle change)
    {
        globals.bUnMuted = muteToggle.isOn;
        unMutedBoolDebug = globals.bUnMuted;
        if (muteToggle.isOn)
        {
            mText.text = "Sound On";
            if(mutedIconSpriteSet)
            {
                mutedIconSprite.enabled = false;
            }
        }
        else
        {
            mText.text = "Sound Off";
            if (mutedIconSpriteSet)
            {
                mutedIconSprite.enabled = true;
            }
        }
    }
}
