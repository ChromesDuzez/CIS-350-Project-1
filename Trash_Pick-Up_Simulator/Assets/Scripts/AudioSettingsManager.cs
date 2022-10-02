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
    public static float volumePerc = 100; //will need to implement a way to change this setting and any other subsequent audio settings but for now we just have mute/unmute
    public static bool bUnMuted = false;
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
        muteToggle = GameObject.FindWithTag("MuteToggle").GetComponent<Toggle>() as Toggle;
        muteToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(muteToggle); 
        });
        mText.text = "Sound On";
        if (mutedIconSprite == null)
        {
            try
            {
                mutedIconSprite = GameObject.FindWithTag("MutedSprite").GetComponent<Image>() as Image;
                mutedIconSpriteSet = true;
                mutedIconSprite.enabled = false;
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
            mutedIconSprite.enabled = false;
        }
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
