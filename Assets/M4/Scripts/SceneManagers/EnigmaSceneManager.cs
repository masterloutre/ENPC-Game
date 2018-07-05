using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnigmaSceneManager : MonoBehaviour
{
    public ValidationMethod validator;
    private PopupManager popm;
    public Score score {get; private set;}


	void Awake () {
        score = new Score();
        // RÉFÉRENCES des managers stockés
        validator = gameObject.GetComponent<ValidationMethod>();
        popm = gameObject.GetComponent<PopupManager>();
        // LISTENERS
        EventManager.instance.AddListener<GOButtonPressedEvent> (submitResult); // En réponse à la question || EnigmaUIManager.GOButtonPressed()
        //EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore); // ?
        EventManager.instance.AddListener<QueryScoreEvent> (sendScore); // Demande la réussite de la question || EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
        //EventManager.instance.AddListener<ValidationScreenEvent>(yourResult); // En réponse à un Popup || PopupManager.submit()
        EventManager.instance.AddListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);
	}

  // SUPPRESSION des listeners une fois terminé
  void OnDestroy () {
	    validator = null;
	    EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
	    //EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);
      EventManager.instance.RemoveListener<QueryScoreEvent>(sendScore);
      //EventManager.instance.RemoveListener<ValidationScreenEvent> (yourResult);
      EventManager.instance.RemoveListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);
  }

    // Lance la phase de Certitude et prévient la création du résultat
	public void enigmaSubmitted(){
        print("ENIGMA SUBMITTED");
        EventManager.instance.Raise(new EnigmaSubmittedEvent());
	}


  //récupère le temps qu'a duré la résolution de l'énigme
	public float getTime(){
		QueryTimerEvent query = new QueryTimerEvent ();
		EventManager.instance.Raise (query);
		return query.time;
	}


    /*

        ////////////////////////
        // GESTION DES EVENTS //
        ////////////////////////


    */

    // Lance la correction de la question et prévient l'affichage de la certitude
    public void submitResult(GOButtonPressedEvent e)
    {
      Debug.Log("GO submit result");
      //score.enigmaSuccess = validator.answerIsRight();
      //score.addEnigmaSuccess(0,validator.score());
      score = validator.fillScore(score);
      score.time = getTime();
      score.help = false;
      popm.setEnigmaSuccess(score.enigmaSuccess);
      popm.updateState("Certitude");
    }

    //event lancé par EnigmaSequenceManager.getEnigmaScore
    public void sendScore(QueryScoreEvent e){
		    e.score = score;
    }

    public void PopUpQuestionsHaveEnded(PopUpQuestionsOverEvent e){
      score.certaintyLevel = popm.certitudeUserInput;
      score.addMethodSuccess(0, 0);
      //certitude = popm.certitudeUserInput;
      //method = popm.methodeUserInput;
      //traité dans EnigmaSequenceManager
      enigmaSubmitted();
    }



}
