using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Component qui gère l'interface utilisateur des énigmes, à savoir les boutons et infos qui sont communs à toutes les énigmes
 */

public class EnigmaUIManager : MonoBehaviour
{

	Enigma enigma; // Données de l'énigme en cours

	void Awake ()
    {
    // Récupère les données de l'énigme en cours
    QueryCurrentEnigmaDataEvent query = new QueryCurrentEnigmaDataEvent ();
		EventManager.instance.Raise (query);
		enigma = query.enigma;
		EventManager.instance.AddListener<RequestDisableEnigmaUIEvent>(disableUI);
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestDisableEnigmaUIEvent>(disableUI);
	}

	void Start(){
        // Initialise le prefab avec les données
        fillEnigmaData();
	}

    // Initialise le prefab avec les données
    void fillEnigmaData(){
		GameObject.Find ("Enigma Data/Enigma Title").GetComponent<Text>().text = enigma.name;
		EnigmaType type = (EnigmaType)enigma.type;
		string text = (type == EnigmaType.INPUT)?"Valeur à entrer" : (type == EnigmaType.STUDY_CASE)? "Cas mécanique" : type.ToString();
		GameObject.Find ("Enigma Data/Enigma Type").GetComponent<Text>().text = "Type : " + text;
		EnigmaDifficulty difficulty = (EnigmaDifficulty)enigma.difficulty;
		GameObject.Find ("Enigma Data/Enigma Difficulty").GetComponent<Text>().text = "Difficulté : " + difficulty;
		GameObject.Find ("Enigma Data/Enigma Skill").GetComponent<Text>().text = enigma.skill.name;
		GameObject.Find("Enigma UI/Timer/cadre").GetComponentInChildren<Text>().text = "Temps estimé : " + enigma.maxTime.ToString() + " min";

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

	public void enableUI()
	{
			GameObject.Find("go_button").GetComponent<Image>().raycastTarget = true;
			GameObject.Find("Next Enigma").GetComponent<Image>().raycastTarget = true;
			GameObject.Find("Return Button").GetComponent<Button>().interactable = true;
	}

	public void disableUI(RequestDisableEnigmaUIEvent e)
	{

			GameObject.Find("go_button").GetComponent<Image>().raycastTarget = false;
			GameObject.Find("Next Enigma").GetComponent<Image>().raycastTarget = false;
			GameObject.Find("Return Button").GetComponent<Button>().interactable = false;
	}


}
