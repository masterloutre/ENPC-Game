using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaSceneManager : MonoBehaviour {
	public ValidationMethod validator;
	private bool success;

	// Use this for initialization
	void Awake () {
		EventManager.instance.AddListener<GOButtonPressedEvent> (submitResult);
		EventManager.instance.AddListener<QueryEnigmaScoreEvent> (sendScore);
		validator = gameObject.GetComponent<ValidationMethod>();
		print ("validator  : " + validator.GetType ().Name);
	}

	void onDestroy () {
		validator = null;
		EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
		EventManager.instance.RemoveListener<QueryEnigmaScoreEvent> (sendScore);

	}

	public void submitResult(GOButtonPressedEvent e){
		print ("validator  : " + validator.GetType ().Name);
		success = validator.answerIsRight ();
		enigmaSubmitted ();
	}

	public void enigmaSubmitted(){
		//traité dans PopUpQuestionManager et EnigmaSequenceManager
		EventManager.instance.Raise (new EnigmaSubmittedEvent ());
	}

	public void sendScore(QueryEnigmaScoreEvent e){
		e.enigmaSuccess = success;
	}
}
