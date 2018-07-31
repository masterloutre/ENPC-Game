using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class LoadEnigmaScenes : MonoBehaviour {

    public int sceneToLoad;
    public string sceneName;
    public string enigmaName;

    // quand le gui est initialisé à la création de la scène, cette méthdoe est appelé toutes les frames (auto)
    private void OnGUI()
    {

        // text info
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 80, 200, 50), "Current Scene: " + (SceneManager.GetActiveScene().name));
        // crée un button d'écoute actif de click
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height -200, 200, 50), "Load Scene " + (enigmaName)))
        {
            // à ce moment précis du jeu, on change de manière de charger une scène
            // why ?
            // avant on faisait simplement SceneManager.LoadScene()
            SceneLoadEvents.sceneOnLoad.UpdateScene(sceneName);
        }
    }
}
