using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

/*
 * Manager qui gère une séquence d'énigme
 * Il connait la compétence évaluée, la liste d'énigme à faire jouer ainsi que l'id de l'énigme courante
 * Il collecte le score de l'énigme et des questions de certitude et 
 * C'est lui qui est chargé de loader et unloader les enigmes
 */ 

public class EnigmaSequenceManager : MonoBehaviour {

	private List<EnigmaData> enigmaDataList; // la liste d'énigmes
	public Skill skill; // la compétence évaluée
	private SceneLoader sl;  // le loader de scenes
	private int currentEnigmaId; // l'id de l'énigme en cours
	private bool currentEnigmaSuccess; //le score de l'énigme en cours
	private float currentEnigmaPopUpQuestionsScore; //le score des questions popup

	//Instancie l'objet et ajoute les listeners
	void Awake(){
        print(" --------------- AWAKING ESM ------------------");
        print(gameObject.scene.name);
		enigmaDataList = new List<EnigmaData> ();
		EventManager.instance.AddListener<RequestNextEnigmaEvent> (loadNextEnigma);
		EventManager.instance.AddListener<RequestPreviousEnigmaEvent> (loadPreviousEnigma);
		EventManager.instance.AddListener<QueryCurrentEnigmaDataEvent> (sendCurrentEnigmaData);
		EventManager.instance.AddListener<EnigmaSubmittedEvent> (getEnigmaScore);

	}
		
	//Load la première énigme
	void Start () {
		QuerySceneLoaderEvent query = new QuerySceneLoaderEvent ();
		EventManager.instance.Raise (query);
		sl = query.sceneLoader;
		currentEnigmaId = 0;
		/*
		foreach (EnigmaData ed in enigmaDataList) {
			Debug.Log ("EnigmaSequenceManager, nom d'énigme : " + ed.nom);
		}
		*/
		StartCoroutine(sl.loadEnigma (enigmaDataList[currentEnigmaId].index_unity));
		//StartCoroutine(sl.loadEnigma (1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Enlève les listener à la destruction du component
	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestNextEnigmaEvent> (loadNextEnigma);
		EventManager.instance.RemoveListener<RequestPreviousEnigmaEvent> (loadPreviousEnigma);
		EventManager.instance.RemoveListener<QueryCurrentEnigmaDataEvent> (sendCurrentEnigmaData);
		EventManager.instance.RemoveListener<EnigmaSubmittedEvent> (getEnigmaScore);
	}

	//Initialise la compétence évaluée et la liste d'énigme 
	//Méthode appelée par GlobalManager entre Awake et Start
	public void updateEnigmaSequence(Skill _skill){
		skill = _skill;
		GameObject titleGO = GameObject.Find ("Title");
		updateEnigmaList ();
	}

	//Récupère la liste d'énigme évaluant la compétence qui a été selectionnée
	public void updateEnigmaList(){
		QueryEnigmaListEvent query = new QueryEnigmaListEvent (skill);
		EventManager.instance.Raise (query);
		enigmaDataList = query.enigmaList;
	}

	//récupère un objet représentant les datas d'une énigme à partir de son index unity
	private EnigmaData getEnigmaByUnityIndex(int value){
		EnigmaSearch es = new EnigmaSearch (value);
		return this.enigmaDataList.Find (es.unityIndexSearch);
	}

	//récupère l'id (position) d'une énigme dans la liste à partir de son index unity
	public int getIdByUnityIndex(int value){
		EnigmaSearch es = new EnigmaSearch (value);
		return this.enigmaDataList.FindIndex (es.unityIndexSearch);
	}

	//récupère l'id de l'énigme suivante dans la liste à partir de l'index unity d'une énigme
	//envoie une exception si il n'y a pas d'énigme suivante
	public int getNextUnityIndex(int currentUnityIndex){
		int enigmaId = getIdByUnityIndex (currentUnityIndex);
		int nextId = enigmaId + 1;
		if (nextId >= enigmaDataList.Count ()) {
			throw new InvalidOperationException ("This was the last enigma");
		} else {
			return nextId;
		}
	}

	//Calcule l'id de la prochaine énigme dans la liste
	//Renvoie une exception si on est arrivés à la fin de la liste
	public int getNextEnigmaId(){
		int nextId = currentEnigmaId + 1; 
		if (nextId >= enigmaDataList.Count) {
			throw new InvalidOperationException ("This was the last enigma");
		} else {
			return nextId;
		}
	}

	//Calcule l'id de l'énigme précédente dans la liste
	//Renvoie une exception si on est arrivés au début de la liste
	public int getPreviousEnigmaId(){
		int nextId = currentEnigmaId - 1; 
		if (nextId < 0) {
			throw new InvalidOperationException ("This was the first enigma");
		} else {
			return nextId;
		}
	}

	//Crée un objet Score Data à partir du score d'une énigme
	ScoreData createScore(bool success){
		EnigmaData currentEnigma = enigmaDataList [currentEnigmaId];
		int points = (success)? currentEnigma.score_max : 0;
		double time = 0;
		bool help = false;
		return new ScoreData (currentEnigma.id, -1, points, 1, time, help);
	}

	/*EVENTS*/

	//Charge la prochaine énigme
	public void loadNextEnigma(RequestNextEnigmaEvent ev){
		try {
			int nextId = getNextEnigmaId();
			StartCoroutine(sl.unloadEnigma(enigmaDataList[currentEnigmaId].index_unity));
			StartCoroutine(sl.loadEnigma(enigmaDataList[nextId].index_unity));
			currentEnigmaId = nextId;
		} catch (InvalidOperationException e){
			Debug.Log (e.Message);
			//EventManager.instance.Raise (new RequestPreviousSceneEvent("EnigmaSequenceScene",0));
		}
	}


	//Charge l'énigme précédente
	public void loadPreviousEnigma(RequestPreviousEnigmaEvent ev){
		try {
			int previousId = getPreviousEnigmaId();
			StartCoroutine(sl.unloadEnigma(enigmaDataList[currentEnigmaId].index_unity));
			StartCoroutine(sl.loadEnigma(enigmaDataList[previousId].index_unity));
			currentEnigmaId = previousId;
		} catch (InvalidOperationException e){
			Debug.Log (e.Message);
			//EventManager.instance.Raise (new RequestPreviousSceneEvent("EnigmaSequenceScene",0));
		}
	}

	//Envoie l'id de l'énigme courante
	public void sendCurrentEnigmaData(QueryCurrentEnigmaDataEvent e){
		e.enigmaData = enigmaDataList [currentEnigmaId];
	}

	//Récupère le score de l'énigme courante et envoie une demande de sauvegarde
	public void getEnigmaScore(EnigmaSubmittedEvent e){
		//traité dans EnigmaSceneManager
		QueryEnigmaSuccessEvent query = new QueryEnigmaSuccessEvent ();
		EventManager.instance.Raise (query);
		currentEnigmaSuccess = query.enigmaSuccess;
		if (currentEnigmaSuccess) {
			print("ENIGMA VALIDATED !!!!!!!!!!!!!");

		} else {
			print("RESULT FALSE !!!!!!!!!!");
		}

        EventManager.instance.Raise (new RequestSaveScoreEvent (createScore(currentEnigmaSuccess)));

	}
}
