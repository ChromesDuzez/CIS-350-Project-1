/*
* Devun Schneider
* CIS 350 - Trash Pick-Up Simulator
* This script manages the changing of scenes throughout the menus
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("SceneLoader");
        Debug.Log("sceneName to load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}