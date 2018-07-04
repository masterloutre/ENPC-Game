using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
public class Enigma : MonoBehaviour {

    // ce truc est un GO qui contient toutes les valeurs de références concernant l'énigme et qui les assignent aux GO en dessous
    public GameObject dataGO;


    public GameObject estimatedTimeGO;
    public GameObject descriptionGO;
    public GameObject enigmaTitleGO;
    public GameObject enigmaTypeGO;
    public GameObject enigmaDifficultyGO;
    public GameObject timerGO;



    public void enigmaUpdate()
    {
        //Réasignation de nouvelle valeurs provenant de dataGO vers les autres GO de la scène

        //Description (text)
        string description = dataGO.GetComponent<Enigme_Data>().enigmaDescription;
        descriptionGO.GetComponent<UnityEngine.UI.Text>().text = description;
        print("description " + description);

        // Titre de l'énigme (text)
        string title = dataGO.GetComponent<Enigme_Data>().enigmaTitle;
        enigmaTitleGO.GetComponent<UnityEngine.UI.Text>().text = title;
        print("title " + title);

        // Type de l'énigme (text)
        string type = dataGO.GetComponent<Enigme_Data>().enigmaType.ToString();
        enigmaTypeGO.GetComponent<UnityEngine.UI.Text>().text = type;

        // Niveau de difficulté (text)
        string diff = dataGO.GetComponent<Enigme_Data>().enigmaDifficulty.ToString();
        enigmaDifficultyGO.GetComponent<UnityEngine.UI.Text>().text = diff;

        // Numéro ID de l'énigme (int)
        int enigmaId = dataGO.GetComponent<Enigme_Data>().enigmaId;
        ScoreControl.updateId(enigmaId);

        // Nombre d'essai max (int)
        int enigmaMaxAttempts = dataGO.GetComponent<Enigme_Data>().enigmaMaxAttempts;
        ScoreControl.updateMaxAttempts(enigmaMaxAttempts);

        // qui dit nouvelle énigme dit nouveau chrono
        resetTimer();
    }

    public void resetTimer()
    {
        timerGO.GetComponent<Timer>().resetTimer();
    }

    // Récupération des statistiques à la fin de l'énigme
    public void enigmaEnd()
    {

        ScoreControl.saveTime(timerGO.GetComponent<Timer>().getTime());
        Save();
        // Passe à la suite, nouvelle scène
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
            dataGO.GetComponentInChildren<InputAnswer>().validation(); // méthode de validation spécifique au script InputAnswer
        else if (dataGO.GetComponentInChildren<AlgoAnswer>() != null)
            dataGO.GetComponentInChildren<AlgoAnswer>().validation(); // méthode de validation spécifique au script QCM_Answer
    }
}
*/
