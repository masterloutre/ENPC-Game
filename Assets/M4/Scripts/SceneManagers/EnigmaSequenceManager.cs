using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class EnigmaSequenceManager : MonoBehaviour {

	private List<EnigmaData> enigmaDataList;
	public Skill skill;
	private SceneLoader sl;
	private int currentEnigmaId;
	private bool currentEnigmaSuccess;
	private float currentEnigmaPopUpQuestionsScore;


	void Awake(){
		enigmaDataList = new List<EnigmaData> ();
		EventManager.instance.AddListener<RequestNextEnigmaEvent> (loadNextEnigma);
		EventManager.instance.AddListener<RequestPreviousEnigmaEvent> (loadPreviousEnigma);
		EventManager.instance.AddListener<QueryCurrentEnigmaDataEvent> (getCurrentEnigmaData);
		EventManager.instance.AddListener<EnigmaSubmittedEvent> (getEnigmaScore);

	}

	// Use this for initialization
	void Start () {
		QuerySceneLoaderEvent query = new QuerySceneLoaderEvent ();
		EventManager.instance.Raise (query);
		sl = query.sceneLoader;
		currentEnigmaId = 0;
		foreach (EnigmaData ed in enigmaDataList) {
			Debug.Log ("EnigmaSequenceManager, nom d'énigme : " + ed.nom);
		}
		StartCoroutine(sl.loadEnigma (enigmaDataList[currentEnigmaId].index_unity));
		//StartCoroutine(sl.loadEnigma (1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestNextEnigmaEvent> (loadNextEnigma);
		EventManager.instance.RemoveListener<RequestPreviousEnigmaEvent> (loadPreviousEnigma);
		EventManager.instance.RemoveListener<QueryCurrentEnigmaDataEvent> (getCurrentEnigmaData);
		EventManager.instance.RemoveListener<EnigmaSubmittedEvent> (getEnigmaScore);
	}

	//arguments temporaire pour test 
	public void updateEnigmaSequence(Skill _skill){
		skill = _skill;
		Debug.Log ("update enigma data : " + skill.name);
		GameObject titleGO = GameObject.Find ("Title");
		updateEnigmaList ();


	}

	public void updateEnigmaList(){
		Debug.Log ("update enigma list : " + skill.name);
		QueryEnigmaListEvent query = new QueryEnigmaListEvent (skill);
		EventManager.instance.Raise (query);
		enigmaDataList = query.enigmaList;
		Debug.Log ("Enigma data list finished loading");
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

	public int getNextEnigmaId(){
		int nextId = currentEnigmaId + 1; 
		if (nextId >= enigmaDataList.Count) {
			throw new InvalidOperationException ("This was the last enigma");
		} else {
			return nextId;
		}
	}

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

	public int getPreviousEnigmaId(){
		int nextId = currentEnigmaId - 1; 
		if (nextId < 0) {
			throw new InvalidOperationException ("This was the first enigma");
		} else {
			return nextId;
		}
	}

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

	public void getCurrentEnigmaData(QueryCurrentEnigmaDataEvent e){
		e.enigmaData = enigmaDataList [currentEnigmaId];
	}

	public void getEnigmaScore(EnigmaSubmittedEvent e){
		//traité dans EnigmaSceneManager
		QueryEnigmaScoreEvent query = new QueryEnigmaScoreEvent ();
		EventManager.instance.Raise (query);
		currentEnigmaSuccess = query.enigmaSuccess;
		if (currentEnigmaSuccess) {
			print("ENIGMA VALIDATED !!!!!!!!!!!!!");
		} else {
			print("RESULT FASLE !!!!!!!!!!");
		}

	}
		
}
