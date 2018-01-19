using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnigmaManager : MonoBehaviour {

    //int currentEnigmaIndex;

    public GameObject enigma;
    public GameObject estimatedTimeGO;
    public GameObject descriptionGO;
    public GameObject enigmaTitleGO;
    public GameObject enigmaTypeGO;
    public GameObject enigmaDifficultyGO;
    public GameObject timerGO;
    public string nextSceneName;

    // Use this for initialization
    public void Start () {
        
    }

    // Update is called once per frame
    
    public void Update () {
        
    }

    public void enigmaCallValidation()
    {
        print("calling validation");
        if (enigma.GetComponentInChildren<InputAnswer>() != null)
            enigma.GetComponentInChildren<InputAnswer>().validation();
        else if (enigma.GetComponentInChildren<QCM_Answer>() != null)
            enigma.GetComponentInChildren<QCM_Answer>().validation();
    }

    public void enigmaUpdate()
    {
        string description = enigma.GetComponent<Enigme_Data>().enigmaDescription;
        descriptionGO.GetComponent<UnityEngine.UI.Text>().text = description;

        string title = enigma.GetComponent<Enigme_Data>().enigmaTitle;
        enigmaTitleGO.GetComponent<UnityEngine.UI.Text>().text = title;

        string type = enigma.GetComponent<Enigme_Data>().enigmaType.ToString();
        enigmaTypeGO.GetComponent<UnityEngine.UI.Text>().text = type;

        string diff = enigma.GetComponent<Enigme_Data>().enigmaDifficulty.ToString();
        enigmaDifficultyGO.GetComponent<UnityEngine.UI.Text>().text = diff;
    }

    public static void enigmaEnd()
    {
        Save();
        SceneManager.LoadScene("default_scene");
    }

    public static void Save()
    {
        DataControl.control.Save();
    }
}