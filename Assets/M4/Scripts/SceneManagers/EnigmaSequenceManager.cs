using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class EnigmaSequenceManager : MonoBehaviour {

	private List<EnigmaData> enigmaDataList;
	public Skill skill;
	private SceneLoader sl;
	private int currentEnigmaId;


	void Awake(){
		QuerySceneLoaderEvent query = new QuerySceneLoaderEvent ();
		EventManager.instance.Raise (query);
		sl = query.sceneLoader;
		currentEnigmaId = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//arguments temporaire pour test 
	public void updateEnigmaSequence(Skill _skill){
		skill = _skill;
		Debug.Log ("update enigma data : " + skill.name);
		GameObject titleGO = GameObject.Find ("Title");
		titleGO.GetComponent<Text> ().text += skill.name;
		updateEnigmaList ();
		foreach (EnigmaData ed in enigmaDataList) {
			Debug.Log ("EnigmaSequenceManager, nom d'énigme : " + ed.nom);
		}
		loadNextEnigma ();


	}

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

	public int getNextEnigmaId(){
		int nextId = currentEnigmaId + 1; 
		if (nextId >= enigmaDataList.Count ()) {
			throw new InvalidOperationException ("This was the last enigma");
		} else {
			return nextId;
		}
	}

	public void loadNextEnigma(){
		Debug.Log ("EnigmaSequenceManager : Load Next Enigme");
		try {
			//FUTURE VERSION
			/*
			int nextId = getNextEnigmaId();
			sl.unloadEnigma(enigmaDataList[currentEnigmaId].index_unity);
			sl.loadEnigma(enigmaDataList[nextId].index_unity);
			currentEnigmaId = nextId;
			*/


			//POUR LES TEST 
			sl.loadEnigma(4);
		} catch (InvalidOperationException e){
			EventManager.instance.Raise (new RequestPreviousSceneEvent("EnigmaSequenceScene",0));
		}

	}
		
}
