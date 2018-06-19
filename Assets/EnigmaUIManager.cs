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

	public void nextEnigma(){
		EventManager.instance.Raise (new RequestNextEnigmaEvent ());
	}

	public void previousEnigma(){
		EventManager.instance.Raise (new RequestPreviousEnigmaEvent ());
	}

	public void submitResult(){
	}

	void fillEnigmaData(){
		Debug.Log("GAME OBJECT : " + GameObject.Find ("Enigma Data/Enigma Title") );
		Debug.Log("ENIGMA : " + enigma );
		GameObject.Find ("Enigma Data/Enigma Title").GetComponent<Text>().text = enigma.nom;
		EnigmaType type = (EnigmaType)enigma.type;
		GameObject.Find ("Enigma Data/Enigma Type").GetComponent<Text>().text = "Type d'énigme : " + type;
		EnigmaDifficulty difficulty = (EnigmaDifficulty)enigma.difficulte;
		GameObject.Find ("Enigma Difficulty").GetComponent<Text>().text = "Difficulté de l'énigme : " + difficulty;
	}
}
