using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enigma : MonoBehaviour {
    public GameObject dataGO;
    public GameObject estimatedTimeGO;
    public GameObject descriptionGO;
    public GameObject enigmaTitleGO;
    public GameObject enigmaTypeGO;
    public GameObject enigmaDifficultyGO;
    public GameObject timerGO;

    public void enigmaUpdate()
    {
        print("enigmaUpdate...");
        string description = dataGO.GetComponent<Enigme_Data>().enigmaDescription;
        descriptionGO.GetComponent<UnityEngine.UI.Text>().text = description;
        print("description " + description);
        string title = dataGO.GetComponent<Enigme_Data>().enigmaTitle;
        enigmaTitleGO.GetComponent<UnityEngine.UI.Text>().text = title;
        print("title " + title);
        string type = dataGO.GetComponent<Enigme_Data>().enigmaType.ToString();
        enigmaTypeGO.GetComponent<UnityEngine.UI.Text>().text = type;

        string diff = dataGO.GetComponent<Enigme_Data>().enigmaDifficulty.ToString();
        enigmaDifficultyGO.GetComponent<UnityEngine.UI.Text>().text = diff;

        int enigmaId = dataGO.GetComponent<Enigme_Data>().enigmaId;
        ScoreControl.updateId(enigmaId);

        int enigmaMaxAttempts = dataGO.GetComponent<Enigme_Data>().enigmaMaxAttempts;
        ScoreControl.updateMaxAttempts(enigmaMaxAttempts);
        resetTimer();
    }

    public void resetTimer()
    {
        timerGO.GetComponent<Timer>().resetTimer();
    }

    public void enigmaEnd()
    {
        ScoreControl.saveTime(timerGO.GetComponent<Timer>().getTime());
        Save();
        SceneManager.LoadScene("default_scene");
    }

    public void Save()
    {
        DataControl.control.Save();
    }

    public void enigmaCallValidation()
    {
        print("calling validation");
        if (dataGO.GetComponentInChildren<InputAnswer>() != null)
            dataGO.GetComponentInChildren<InputAnswer>().validation();
        else if (dataGO.GetComponentInChildren<QCM_Answer>() != null)
            dataGO.GetComponentInChildren<QCM_Answer>().validation();
    }
}
