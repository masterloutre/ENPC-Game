﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnigmaSceneManager : MonoBehaviour
{
    public ValidationMethod validator;
    private PopupManager popm;
    public Score score {get; private set;}
    public bool methodQuestions = true;


	void Awake () {
        score = new Score();
        // RÉFÉRENCES des managers stockés
        validator = gameObject.GetComponent<ValidationMethod>();
        popm = GameObject.Find("Responsive Canvas").GetComponentInChildren<PopupManager>(true);
        popm.gameObject.SetActive(false);
        // LISTENERS
        EventManager.instance.AddListener<GOButtonPressedEvent> (submitEnigmaResult); // En réponse à la question || EnigmaUIManager.GOButtonPressed()
        //EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore); // ?
        EventManager.instance.AddListener<QueryScoreEvent> (sendScore); // Demande la réussite de la question || EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
        //EventManager.instance.AddListener<ValidationScreenEvent>(yourResult); // En réponse à un Popup || PopupManager.submit()
        EventManager.instance.AddListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);
	}

  // SUPPRESSION des listeners une fois terminé
  void OnDestroy () {
	    validator = null;
	    EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitEnigmaResult);
	    //EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);
      EventManager.instance.RemoveListener<QueryScoreEvent>(sendScore);
      //EventManager.instance.RemoveListener<ValidationScreenEvent> (yourResult);
      EventManager.instance.RemoveListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);
  }

    // prévient la création du résultat
	public void enigmaSubmitted(){
        print("ENIGMA SUBMITTED");
        print(score.ToString());
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
    public void submitEnigmaResult(GOButtonPressedEvent e)
    {
      Debug.Log("----> GO -> submitEnigmaResult, EnigmaSceneManager");
      score = validator.fillScore(score);
      print(score.ToString());
      score.time = getTime();
      score.help = false;
      if(popm == null){
        score.certaintyLevel = 100;
        enigmaSubmitted();
      } else {
        popm.setScoreFeedback(validator.getScoreFeedback());
        popm.beginPopUpQuestionsSequence(score);
      }

    }

    //event lancé par EnigmaSequenceManager.getEnigmaScore
    public void sendScore(QueryScoreEvent e){
		    e.score = score;
    }

    public void PopUpQuestionsHaveEnded(PopUpQuestionsOverEvent e){
      score = popm.getScore();
      enigmaSubmitted();
    }


}
