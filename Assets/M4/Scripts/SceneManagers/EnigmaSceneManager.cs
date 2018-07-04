using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnigmaSceneManager : MonoBehaviour
{
    private bool success;
    private float score;
    private float certitude;
    private string method;
    public ValidationMethod validator;
    private PopupManager popm;


	void Awake () {

        // RÉFÉRENCES des managers stockés
        validator = gameObject.GetComponent<ValidationMethod>();
        popm = gameObject.GetComponent<PopupManager>();
        // LISTENERS
        EventManager.instance.AddListener<GOButtonPressedEvent> (submitResult); // En réponse à la question || EnigmaUIManager.GOButtonPressed()
        //EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore); // ?
        EventManager.instance.AddListener<QueryEnigmaSuccessEvent> (sendScore); // Demande la réussite de la question || EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
        //EventManager.instance.AddListener<ValidationScreenEvent>(yourResult); // En réponse à un Popup || PopupManager.submit()
        EventManager.instance.AddListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);

	}

  // SUPPRESSION des listeners une fois terminé
  void OnDestroy () {
	    validator = null;
	    EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
	    //EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);
      EventManager.instance.RemoveListener<QueryEnigmaSuccessEvent>(sendScore);
      //EventManager.instance.RemoveListener<ValidationScreenEvent> (yourResult);
      EventManager.instance.RemoveListener<PopUpQuestionsOverEvent>(PopUpQuestionsHaveEnded);

  }



    // Lance la phase de Certitude et prévient la création du résultat
	public void enigmaSubmitted(){
        print("ENIGMA SUBMITTED");
        EventManager.instance.Raise(new EnigmaSubmittedEvent());
	}

  public float computeScore( float score, float certainty){
    int deltaMax =(int)Math.Round(( (1/ Math.Exp(Math.Pow(score, 0.83)/25.0)) - 0.58) * 120);
    //int deltaMax = (int)((1/Math.Exp(Math.Pow(score, 0.83)/25.0)) -0.58) *120;
    Debug.Log("max delta certainty = " + deltaMax.ToString());
    float delta = (float)Math.Round((100.0 - certainty) * deltaMax / 100.0);
    float result = score + delta;
    return (result < 0)? 0 : (result > 100)? 100 : result;
  }

    static public void enableUI()
    {
        GameObject.Find("go_button").GetComponent<Image>().raycastTarget = true;
        GameObject.Find("Next Enigma").GetComponent<Image>().raycastTarget = true;
        GameObject.Find("Return Button").GetComponent<Button>().interactable = true;
    }
    static public void disableUI()
    {

        GameObject.Find("go_button").GetComponent<Image>().raycastTarget = false;
        GameObject.Find("Next Enigma").GetComponent<Image>().raycastTarget = false;
        GameObject.Find("Return Button").GetComponent<Button>().interactable = false;
    }

    /*

        ////////////////////////
        // GESTION DES EVENTS //
        ////////////////////////


    */

    // Lance la correction de la question et prévient l'affichage de la certitude
    public void submitResult(GOButtonPressedEvent e)
    {
      Debug.Log("GO");
        success = validator.answerIsRight();
        score = validator.score();
        popm.setEnigmaSuccess(success);
        popm.updateState("Certitude");
    }

    // Affiche l'écran de certitude en fonction de la situation
    /*
    public void yourResult(ValidationScreenEvent e)
    {
        if (e.state == "Certitude")
        {
            certitude = e.confidance;
            if (success)
            {
                popm.updateState("Victoire");
            }
            else
            {
                popm.updateState("Défaite");
            }

        }
        else if (e.state=="Défaite")
        {
            popm.updateState("Correction");
        }
        else if (e.state == "Victoire")
        {
            popm.updateState("Justification");
        }
        else // state soit justif soit correct
        {

            method = e.answer;
            EventManager.instance.Raise(new EnigmaSubmittedEvent());
            // retour menu
        }

    }
*/

    // Donne le résultat de ValidationMethod.isRightAnswer() à EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
    public void sendScore(QueryEnigmaSuccessEvent e){
		    e.enigmaSuccess = success;
        e.certitude = certitude;
        e.method = method;
        //e.score = score;
        e.score = computeScore(score, certitude);
    }

    public void PopUpQuestionsHaveEnded(PopUpQuestionsOverEvent e){
      certitude = popm.certitudeUserInput;
      method = popm.methodeUserInput;
      //traité dans EnigmaSequenceManager
      enigmaSubmitted();
    }



}
