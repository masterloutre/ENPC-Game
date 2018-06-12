using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoText : MonoBehaviour {
	private Text text;
	public bool name;
	public bool studentNumber;
	public bool graduatingYear;
	public bool breakLines;
	public string separator = " ";
	private PlayerManager pm;

	public void Awake(){
		pm = Object.FindObjectOfType<PlayerManager>();
	}

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
		text.text = computeText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	string computeText(){
		string text = "";
		if (breakLines) {
			separator = "\n";
		}
		if (name) {
			text += pm.getPlayerName () + separator;
		}
		if (studentNumber) {
			text += pm.getPlayerStudentNumber () + separator;
		}
		if (graduatingYear) {
			text += pm.getPlayerGraduatingYear () + separator;
		}
		return text;
	}
}
