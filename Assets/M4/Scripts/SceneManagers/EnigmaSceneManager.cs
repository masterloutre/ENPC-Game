using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore); // ?
        EventManager.instance.AddListener<QueryEnigmaSuccessEvent> (sendScore); // Demande la réussite de la question || EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
        EventManager.instance.AddListener<ValidationScreenEvent>(yourResult); // En réponse à un Popup || PopupManager.submit()
        
	}
    // SUPPRESSION des listeners une fois terminé
    void OnDestroy () {
		validator = null;
		EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
		EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);
        EventManager.instance.RemoveListener<QueryEnigmaSuccessEvent>(sendScore);
        EventManager.instance.RemoveListener<ValidationScreenEvent> (yourResult);
    }

    // Lance la phase de Certitude et prévient la création du résultat
	public void enigmaSubmitted(){
        print("ENIGMA SUBMITTED");
        popm.updateState("Certitude");
	}



    /*
    
        ////////////////////////
        // GESTION DES EVENTS //
        ////////////////////////


    */

    // Lance la correction de la question et prévient l'affichage de la certitude 
    public void submitResult(GOButtonPressedEvent e)
    {
        success = validator.answerIsRight();
        score = validator.score();
        enigmaSubmitted();
    }
    // Affiche l'écran de certitude en fonction de la situation
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
    // Donne le résultat de ValidationMethod.isRightAnswer() à EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
    public void sendScore(QueryEnigmaSuccessEvent e){
		e.enigmaSuccess = success;
        e.certitude = certitude;
        e.method = method;
        e.score = score;
    }
    // ?????
    public void sendScore(QueryEnigmaScoreEvent e)
    {

    }
    


}
