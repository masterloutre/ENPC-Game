using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gère le déroulement du jeu
 */
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Remoting;

public class GlobalManager : MonoBehaviour {
	public bool startAtLandingPage = false;
	private PlayerManager playerManager;
	private SceneLoader sceneLoader;
	private EnigmaManager enigmaManager;
	private int gameSessionId;
	private ScoreManager scoreManager;

	//variable statique : url root de l'interface web
	public static string webInterfaceRootURL { 
		//VERSION KEN
		//get { return "http://localhost/enpc-web-interface"; }
		//VERSION LOU
		get { return "http://localhost:8888"; }
	}

	//récupère les références au PlayerManager, au SceneLoader, à l'EnigmaManager 
	//enregistre les listener d'events
	public void Awake(){
		playerManager = gameObject.GetComponent<PlayerManager> ();
		sceneLoader = gameObject.GetComponent<SceneLoader> ();
		enigmaManager = gameObject.GetComponent<EnigmaManager> ();
		scoreManager = gameObject.GetComponent<ScoreManager> ();
		EventManager.instance.AddListener<RequestNextSceneEvent> (nextScene);
		EventManager.instance.AddListener<RequestPreviousSceneEvent> (previousScene);
		EventManager.instance.AddListener<QueryPlayerManagerEvent> (getPlayerManager);
		EventManager.instance.AddListener<QuerySceneLoaderEvent> (getSceneLoader);
		EventManager.instance.AddListener<QuerySkillListEvent> (getSkillList);
		EventManager.instance.AddListener<QueryEnigmaListEvent> (getEnigmaList);
		EventManager.instance.AddListener<RequestSaveScoreEvent> (saveScoreToServer);
	}

	//à l'initialisation du gameObject, lance la séquence de démarrage
	public void Start(){
		Debug.Log ("ATTENTION : l'addresse indiquée pour l'interface est : " + webInterfaceRootURL + " Si ce n'est pas la bonne il faut décommenter la bonne version dans GlobalManager");
		StartCoroutine(startSequence ());
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestNextSceneEvent> (nextScene);
		EventManager.instance.RemoveListener<RequestPreviousSceneEvent> (previousScene);
		EventManager.instance.RemoveListener<QueryPlayerManagerEvent> (getPlayerManager);
		EventManager.instance.RemoveListener<QuerySceneLoaderEvent> (getSceneLoader);
		EventManager.instance.RemoveListener<QuerySkillListEvent> (getSkillList);
		EventManager.instance.RemoveListener<QueryEnigmaListEvent> (getEnigmaList);
		EventManager.instance.RemoveListener<RequestSaveScoreEvent> (saveScoreToServer);

	}

	//Séquence de démarrage, les coroutines permettent d'attendre que la méthode appelée soit entierement executées avant de yield
	//les yield sont effecuté un par un dans l'ordre
	IEnumerator startSequence(){
		yield return StartCoroutine (getSessionId ());
		if (gameSessionId == 0) {
			Debug.Log ("Le jeu n'est pas autorisé");
			yield break;
		}
		yield return StartCoroutine(playerManager.instanciatePlayer());
		yield return StartCoroutine (enigmaManager.instanciateEnigmas ());
		if (startAtLandingPage) {
			yield return StartCoroutine (sceneLoader.loadLandingPage ());
		}
	}

	IEnumerator getSessionId(){
		string serverURL = GlobalManager.webInterfaceRootURL;
		UnityWebRequest getRequest = UnityWebRequest.Get (serverURL + "/index.php?action=session-ouverte");
		yield return getRequest.SendWebRequest();
		if(getRequest.isNetworkError || getRequest.isHttpError) {
			Debug.Log(getRequest.error);
			Debug.Log(getRequest.downloadHandler.text);

		}
		else {
			if (!Int32.TryParse(getRequest.downloadHandler.text, out this.gameSessionId))
			{
				this.gameSessionId = 0;
			}

		}
	}


	/**********************
	 * GESTION DES EVENTS *
	 * ********************/

	//load le prochain menu en fonction du nom de la scène actuelle
	//les coroutines ne sont peut etre pas forcément nécessaire mais on les garde pour pouvoir garder 
	// les fct du scenenLoader avec des valeurs de retour IENumerator pour plus d'homogénéité
	void nextScene(RequestNextSceneEvent e){
		//Debug.Log ("Next scene requested by " + e.currentSceneName);
		if (e.currentSceneName == "HomeScene") {
			StartCoroutine(sceneLoader.loadSkillsMenu ());
			//StartCoroutine(sl.loadEnigma (4));
		} else if (e.currentSceneName == "SelectionScene"){
            print(e.currentSceneName + "|" + e.choiceId);
			Skill chosenSkill = enigmaManager.getSkills () [e.choiceId];
			StartCoroutine (sceneLoader.loadEnigmaSequence (enigmaManager.getSkills()[e.choiceId]));
		}
	}

	void previousScene(RequestPreviousSceneEvent e){
		//Debug.Log ("Previous scene requested by " + e.currentSceneName);
		if (e.currentSceneName == "HomeScene") {
			//fermer le jeu?
		} else if (e.currentSceneName == "SelectionScene") {
			StartCoroutine (sceneLoader.loadLandingPage ());
		} else if (e.currentSceneName == "EnigmaSequenceScene") {
			StartCoroutine (sceneLoader.loadSkillsMenu ());
		}
	}

	void getPlayerManager(QueryPlayerManagerEvent e){
		e.playerManager = this.playerManager;
	}

	void getSceneLoader(QuerySceneLoaderEvent e){
		e.sceneLoader = this.sceneLoader;
	}

	void getSkillList(QuerySkillListEvent e ){
		try{
			e.skillList = enigmaManager.getSkills ();
		} catch (Exception exception) {
			throw exception;
		}
	}

	void getEnigmaList(QueryEnigmaListEvent e ){
		try{
			if(e.skill != null){
				e.enigmaList = enigmaManager.getEnigmasBySkill(e.skill);
			} else {
				e.enigmaList = enigmaManager.getEnigmas();
			}
		} catch (Exception exception) {
			throw exception;
		}
	}

	void saveScoreToServer(RequestSaveScoreEvent e){
		e.score.id_etudiant = playerManager.getPlayerId ();
		scoreManager.saveScoreToServer (e.score);
	}
}