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
}