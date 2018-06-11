using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadEvents : MonoBehaviour {

    public static SceneLoadEvents sceneOnLoad;
    //public Scene scene;

    void Awake()
    {
        print("SceneLoadEvents est appelé (Awake)");
        if (sceneOnLoad == null)
        {
            DontDestroyOnLoad(gameObject);
            sceneOnLoad = this;
        }
        else if (sceneOnLoad != this)
        {
            Destroy(gameObject);
        }
    }
    public void load(string scenename)
    {
        SceneManager.LoadScene(scenename);
        print("SceneLoadEvents est appelé (Load)");
    }
    public void UpdateScene(string sceneName)
    {
        print("SceneLoadEvents est appelé (UpdateScene)");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        // ??? c koi OnSceneLoaded ?
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // ???
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("SceneLoadEvents est appelé (OnSceneLoaded)");
        //???
        SceneManager.SetActiveScene(scene);
        print("OnSceneLoaded " + scene.name);
        print("Active: " + SceneManager.GetActiveScene().name);

        //load the saved information to in the new scene using datacontrol
        DataControl.control.Load();

        foreach (GameObject GO in scene.GetRootGameObjects())
        {
            print("Checking RootGameObjects in Scene " + SceneManager.GetActiveScene().name+": "+GO.name);
            
            // check l'existence d'une énigme et si oui l'update
            // why do that ?
            // Scénario comprenant un update d'énigme ?
            if (GO.GetComponentInChildren<Enigma>() != null)
            {
                //get scene's Enigma
                print("cetteligne sert");
                GO.GetComponentInChildren<Enigma>().enigmaUpdate();
            }
        }
    }
}
