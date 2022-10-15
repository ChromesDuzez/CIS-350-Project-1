/*
* Zach Wilson
* CIS 350 - Group Project
* This script updates the sensitivity variable in globalSettings
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateSense : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            GlobalSettings.mouseSense = v;
        });
    }
}
