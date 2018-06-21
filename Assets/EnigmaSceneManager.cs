using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaSceneManager : MonoBehaviour {
	public ValidationMethod validator;
	private bool success;

	// Use this for initialization
	void Awake () {
		EventManager.instance.AddListener<GOButtonPressedEvent> (submitResult);
		validator = gameObject.GetComponent<ValidationMethod>();
	}

	void onDestroy () {
		EventManager.instance.RemoveListener<GOButtonPressedEvent> (submitResult);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void submitResult(GOButtonPressedEvent e){
		print ("Go button pressed");
		success = validator.answerIsRight ();
	}
}
