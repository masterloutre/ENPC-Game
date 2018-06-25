using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Component qui gère l'interface utilisateur des énigmes, à savoir les boutons et infos qui sont communs à toutes les énigmes
 */ 

public class EnigmaUIManager : MonoBehaviour {
	EnigmaData enigma;

	// Récupère les datas de l'énigme en cours
	void Awake () {
		QueryCurrentEnigmaDataEvent query = new QueryCurrentEnigmaDataEvent ();
		EventManager.instance.Raise (query);
		enigma = query.enigmaData;

	}

	//Démarre le component en remplissant le préfab
	void Start(){
		fillEnigmaData ();
	}

	//Rempli le préfab avec les informations de l'énigme
	void fillEnigmaData(){
		GameObject.Find ("Enigma Data/Enigma Title").GetComponent<Text>().text = enigma.nom;
		EnigmaType type = (EnigmaType)enigma.type;
		GameObject.Find ("Enigma Data/Enigma Type").GetComponent<Text>().text = "Type d'énigme : " + type;
		EnigmaDifficulty difficulty = (EnigmaDifficulty)enigma.difficulte;
		GameObject.Find ("Enigma Data/Enigma Difficulty").GetComponent<Text>().text = "Difficulté de l'énigme : " + difficulty;
	}

	//EVENTS BUTTONS//
	//Toutes ces méthodes décrivent les events qui doivent être envoyés quand les bouttons sont cliqués

	public void nextEnigma(){
		//Traité dans EnigmaSequenceManager
		EventManager.instance.Raise (new RequestNextEnigmaEvent ());
	}

	public void previousEnigma(){
		//Traité dans EnigmaSequenceManager
		EventManager.instance.Raise (new RequestPreviousEnigmaEvent ());
	}

	public void GOButtonPressed(){
		//Traité dans EnigmaSceneManager
		EventManager.instance.Raise (new GOButtonPressedEvent ());
	}

	public void iButtonPressed(){
		//Pas encore utilisé
		EventManager.instance.Raise (new iButtonPressedEvent ());
	}

	public void targetButtonPressed(){
		//Pas encore utilisé
		EventManager.instance.Raise (new targetButtonPressedEvent ());
	}


}
