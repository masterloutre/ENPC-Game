using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaSceneManager : MonoBehaviour {
	public ValidationMethod validator;
	private bool success;
    private PopupManager popm;
	// Use this for initialization
	void Awake () {
        popm = new PopupManager();

        
        EventManager.instance.AddListener<GOButtonPressedEvent> (submitResult);
		EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore);

        EventManager.instance.AddListener<ValidationScreenEvent>(yourResult); // coming from PopupManager.submit() (likely from a submit button ) | Contains answer only from additional questions post-enigma

        validator = gameObject.GetComponent<ValidationMethod>();
	}

	void onDestroy () {
		EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
		EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);

	}

	public void submitResult(GOButtonPressedEvent e){
		success = validator.answerIsRight ();
		enigmaSubmitted ();
	}

	public void enigmaSubmitted(){
        //traité dans PopUpQuestionManager et EnigmaSequenceManager
        popm.updateState("Justification");
	}
    public void yourResult(ValidationScreenEvent e)
    {
        if (e.state == "Justification")
        {
            if (success)
            {
                popm.updateState("Victoire");
            }
            else
            {
                popm.updateState("Défaite");
            }

            // chopper le score

        }
        else if (e.state=="Défaite")
        {
            popm.updateState("Correction");
        }
        else // correction ou victoire
        {
            // fin d'énigme
        }
        
    }
	public void sendScore(QueryEnigmaScoreEvent e){
		e.enigmaSuccess = success;
	}

    

    
}
