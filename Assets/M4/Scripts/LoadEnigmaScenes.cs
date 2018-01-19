using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEnigmaScenes : MonoBehaviour {

    public int sceneToLoad;
    public string sceneName;
    public string enigmaName;

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 80, 200, 50), "Current Scene: " + (SceneManager.GetActiveScene().name));
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height -200, 200, 50), "Load Scene " + (enigmaName)))
        {
            SceneLoadEvents.sceneOnLoad.UpdateScene(sceneName);
        }
    }
}
