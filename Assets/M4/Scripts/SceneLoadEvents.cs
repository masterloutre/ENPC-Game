using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadEvents : MonoBehaviour {

    public static SceneLoadEvents sceneOnLoad;
    public Scene scene;

    void Awake()
    {
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

    // Use this for initialization
    public void Start () {

	}


    // Update is called once per frame
    void Update () {

    }

    public void UpdateScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
        scene = SceneManager.GetSceneByName(sceneName);
        print("Loading Scene " + scene.name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        print("OnSceneLoaded " + scene.name);
        print("Active: " + SceneManager.GetActiveScene().name);
        //load the saved information to in the new scene using datacontrol
        DataControl.control.Load();

        foreach (GameObject GO in scene.GetRootGameObjects())
        {
            //print("Checking RootGameObjects in Scene " + SceneManager.GetActiveScene().name);
            if (GO.GetComponentInChildren<Enigma>() != null)
            {
                //get scene's Enigma
                GO.GetComponentInChildren<Enigma>().enigmaUpdate();
            }
        }
    }
}
