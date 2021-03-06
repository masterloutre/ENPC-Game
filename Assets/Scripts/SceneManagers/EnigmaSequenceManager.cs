﻿using System.Collections;
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

public class EnigmaSequenceManager : MonoBehaviour
{

    private int currentEnigmaId; // l'id de l'énigme en cours
    private Score currentEnigmaScore; //le score de l'énigme en cours
    private float currentEnigmaPopUpQuestionsScore; //le score des questions popup en cours

    private List<EnigmaData> enigmaDataList; // la liste d'énigmes
    private List<Enigma> enigmaList;
	  public Skill skill; // la compétence évaluée
	  private SceneLoader sl;  // le loader de scène

	void Awake(){
        skill = null;
		    enigmaDataList = new List<EnigmaData> ();
        enigmaList = new List<Enigma> ();

        // LISTENERS
		    EventManager.instance.AddListener<RequestNextEnigmaEvent> (loadNextEnigma); // Passer à l'énigme suivante || EnigmaUIManager.nextEnigma()
        EventManager.instance.AddListener<RequestPreviousEnigmaEvent> (loadPreviousEnigma); // Passer à l'énigme précédente || EnigmaUIManager.previousEnigma()
        EventManager.instance.AddListener<QueryCurrentEnigmaDataEvent> (sendCurrentEnigmaData); // Demande des données de l'énigme en cours || EnigmaUIManager.Awake()
        EventManager.instance.AddListener<EnigmaSubmittedEvent> (getEnigmaScore); // Création du score || EnigmaSceneManager.submitResult(GOButtonPressedEvent)

    }
    // SUPPRESSION des listeners une fois terminé
    void OnDestroy()
    {
        EventManager.instance.RemoveListener<RequestNextEnigmaEvent>(loadNextEnigma);
        EventManager.instance.RemoveListener<RequestPreviousEnigmaEvent>(loadPreviousEnigma);
        EventManager.instance.RemoveListener<QueryCurrentEnigmaDataEvent>(sendCurrentEnigmaData);
        EventManager.instance.RemoveListener<EnigmaSubmittedEvent>(getEnigmaScore);
    }

    //Load la première énigme
    void Start () {
    // Récupère la référence du SceneLoader de GlobalManager
		QuerySceneLoaderEvent query = new QuerySceneLoaderEvent ();
		EventManager.instance.Raise (query);
		sl = query.sceneLoader;
    updateEnigmaList ();
		currentEnigmaId = 0;
		StartCoroutine(sl.loadEnigma (enigmaDataList[currentEnigmaId].index_unity));
	}


	// Initialise la compétence évaluée et la liste d'énigme
	// appelée par GlobalManager entre Awake et Start
	public void updateEnigmaSequence(Skill _skill){
		skill = _skill;
		//GameObject titleGO = GameObject.Find ("Title");
		//updateEnigmaList ();
	}

	//Récupère la liste d'énigme évaluant la compétence qui a été selectionnée
	public void updateEnigmaList(){
		//QueryEnigmaListEvent query = new QueryEnigmaListEvent (skill);
		QueryEnigmaListEvent query = new QueryEnigmaListEvent ();
		EventManager.instance.Raise (query);
		enigmaDataList = query.enigmaList;
    foreach(EnigmaData data in query.enigmaList){
      enigmaList.Add(new Enigma(data));
    }

	}

    //Calcule l'id de la prochaine énigme dans la liste
    //Renvoie une exception si on est arrivés à la fin de la liste
    public int getNextEnigmaId()
    {
        int nextId = currentEnigmaId + 1;
        if (nextId >= enigmaDataList.Count)
        {
            throw new InvalidOperationException("This was the last enigma");
        }
        else
        {
            return nextId;
        }
    }
    //Calcule l'id de l'énigme précédente dans la liste
    //Renvoie une exception si on est arrivés au début de la liste
    public int getPreviousEnigmaId()
    {
        int nextId = currentEnigmaId - 1;
        if (nextId < 0)
        {
            throw new InvalidOperationException("This was the first enigma");
        }
        else
        {
            return nextId;
        }
    }

/*
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

*/
	/*EVENTS*/

	//Charge la prochaine énigme
	public void loadNextEnigma(RequestNextEnigmaEvent ev){
    EventManager.instance.Raise(new RequestEnigmaRemoved(enigmaDataList[currentEnigmaId]));
		try {
			int nextId = getNextEnigmaId();
			StartCoroutine(sl.unloadEnigma(enigmaDataList[currentEnigmaId].index_unity));
			StartCoroutine(sl.loadEnigma(enigmaDataList[nextId].index_unity));
			currentEnigmaId = nextId;
		} catch (InvalidOperationException e){
      StartCoroutine(sl.loadPopUp("EndOfEnigmaSequencePopUp"));
			//Debug.Log ("Erreur chopée dans enigma sequence manager" + e.Message);
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

	//Envoie les informations de l'énigme courante
	public void sendCurrentEnigmaData(QueryCurrentEnigmaDataEvent e){
		e.enigma = enigmaList [currentEnigmaId];
	}

	//Récupère le score de l'énigme courante et envoie une demande de sauvegarde
	public void getEnigmaScore(EnigmaSubmittedEvent e){
		//on récupère le succès qui est traité dans EnigmaSceneManager

    QueryScoreEvent query = new QueryScoreEvent();

		//QueryEnigmaSuccessEvent query = new QueryEnigmaSuccessEvent ();
		EventManager.instance.Raise (query);
    //après ces deux lignes, l'objet query a été updaté dans EnigmaSceneManager.sendScore();
		currentEnigmaScore = query.score;
    currentEnigmaScore.maxNumberPoints = enigmaList[currentEnigmaId].maxScore;
    EventManager.instance.Raise (new RequestSaveScoreEvent (new ScoreData(enigmaList[currentEnigmaId].dbId, -1, currentEnigmaScore)));
    loadNextEnigma(null);

	}

}
