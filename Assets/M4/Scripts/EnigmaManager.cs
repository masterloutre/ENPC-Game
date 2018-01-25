using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnigmaManager : MonoBehaviour {

    public static void enigmaEnd()
    {
        foreach (GameObject GO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            //print("Checking RootGameObjects in Scene " + SceneManager.GetActiveScene().name);
            if (GO.GetComponentInChildren<Enigma>() != null)
            {
                //get scene's Enigma
                GO.GetComponentInChildren<Enigma>().enigmaEnd();
                print("end enigma");
            }
        }
    }
}