using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour {

    public void HelpRequest()
    {
        DataControl.control.aideExt = true;
    }

    public static void validationRequest()
    {
        DataControl.control.tentatives += 1;
    }

    public static bool maxAttempsReached()
    {
        if (DataControl.control.tentatives >= DataControl.control.enigmaMaxAttempts)
        {
            return true;
        }
        return false;
    }

    public static void Success(bool success)
    {
        DataControl.control.enigmeReussie = success;
    }

    public static void saveTime(float time)
    {
        DataControl.control.temps = time;
    }

    public static void updateId(int enigmaId)
    {
        DataControl.control.enigmaId = enigmaId;
    }

    public static void updateMaxAttempts(int enigmaMaxAttempts)
    {
        DataControl.control.enigmaMaxAttempts = enigmaMaxAttempts;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
