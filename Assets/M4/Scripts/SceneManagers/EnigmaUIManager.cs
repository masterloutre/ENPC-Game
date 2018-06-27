using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Component qui gère l'interface utilisateur des énigmes, à savoir les boutons et infos qui sont communs à toutes les énigmes
 */ 

public class EnigmaUIManager : MonoBehaviour
{

	EnigmaData enigma; // Données de l'énigme en cours
	
	void Awake ()
    {
        // Récupère les données de l'énigme en cours
        QueryCurrentEnigmaDataEvent query = new QueryCurrentEnigmaDataEvent ();
		EventManager.instance.Raise (query);
		enigma = query.enigmaData;
	}

	void Start(){
        // Initialise le prefab avec les données
        fillEnigmaData();
	}

    // Initialise le prefab avec les données
    void fillEnigmaData(){
		GameObject.Find ("Enigma Data/Enigma Title").GetComponent<Text>().text = enigma.nom;
		EnigmaType type = (EnigmaType)enigma.type;
		GameObject.Find ("Enigma Data/Enigma Type").GetComponent<Text>().text = "Type d'énigme : " + type;
		EnigmaDifficulty difficulty = (EnigmaDifficulty)enigma.difficulte;
		GameObject.Find ("Enigma Data/Enigma Difficulty").GetComponent<Text>().text = "Difficulté de l'énigme : " + difficulty;
	}

	//EVENTS BUTTONS//
	//Toutes ces méthodes décrivent les events qui sont envoyés quand les boutons sont cliqués

    // Passer à l'énigme suivante
	public void nextEnigma(){
		//Traité dans EnigmaSequenceManager
		EventManager.instance.Raise (new RequestNextEnigmaEvent ());
	}
    // Passer à l'énigme précédente
    public void previousEnigma(){
		//Traité dans EnigmaSequenceManager
		EventManager.instance.Raise (new RequestPreviousEnigmaEvent ());
	}
    // Valider la réponse
    public void GOButtonPressed(){
		//Traité dans EnigmaSceneManager
		EventManager.instance.Raise (new GOButtonPressedEvent ());
	}
    // Voir les informations/énoncés de l'énigme
	public void iButtonPressed(){
		//Pas encore utilisé
		EventManager.instance.Raise (new iButtonPressedEvent ());
	}
    // ???
	public void targetButtonPressed(){
		//Pas encore utilisé
		EventManager.instance.Raise (new targetButtonPressedEvent ());
	}


}
