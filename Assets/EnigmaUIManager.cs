using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnigmaUIManager : MonoBehaviour {
	EnigmaData enigma;

	// Use this for initialization
	void Awake () {
		QueryCurrentEnigmaDataEvent query = new QueryCurrentEnigmaDataEvent ();
		EventManager.instance.Raise (query);
		enigma = query.enigmaData;

	}

	void Start(){
		fillEnigmaData ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void fillEnigmaData(){
		GameObject.Find ("Enigma Data/Enigma Title").GetComponent<Text>().text = enigma.nom;
		EnigmaType type = (EnigmaType)enigma.type;
		GameObject.Find ("Enigma Data/Enigma Type").GetComponent<Text>().text = "Type d'énigme : " + type;
		EnigmaDifficulty difficulty = (EnigmaDifficulty)enigma.difficulte;
		GameObject.Find ("Enigma Data/Enigma Difficulty").GetComponent<Text>().text = "Difficulté de l'énigme : " + difficulty;
	}

	//EVENTS BUTTONS//

	public void nextEnigma(){
		EventManager.instance.Raise (new RequestNextEnigmaEvent ());
	}

	public void previousEnigma(){
		EventManager.instance.Raise (new RequestPreviousEnigmaEvent ());
	}

	public void GOButtonPressed(){
		EventManager.instance.Raise (new GOButtonPressedEvent ());
	}

	public void iButtonPressed(){
		EventManager.instance.Raise (new iButtonPressedEvent ());
	}

	public void targetButtonPressed(){
		EventManager.instance.Raise (new targetButtonPressedEvent ());
	}


}
