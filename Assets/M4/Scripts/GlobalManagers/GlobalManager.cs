using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gère le déroulement du jeu
 */
using UnityEngine.Networking;
using System;

public class GlobalManager : MonoBehaviour {
	public bool startAtLandingPage = false;
	private PlayerManager pm;
	private SceneLoader sl;
	private EnigmaManager em;
	private int gameSessionId;

	//variable statique : url root de l'interface web
	public static string webInterfaceRootURL { 
		get { 
			//VERSION KEN
			//return "http://localhost/enpc-web-interface";
			//VERSION LOU
			return "http://localhost:8888";
		}
	}

	//récupère les références au PlayerManager, au SceneLoader, à l'EnigmaManager 
	//enregistre les listener d'events
	public void Awake(){
		pm = gameObject.GetComponent<PlayerManager> ();
		sl = gameObject.GetComponent<SceneLoader> ();
		em = gameObject.GetComponent<EnigmaManager> ();
		EventManager.instance.AddListener<RequestNextSceneEvent> (nextScene);
		EventManager.instance.AddListener<RequestPreviousSceneEvent> (previousScene);
		EventManager.instance.AddListener<QueryPlayerManagerEvent> (getPlayerManager);
		EventManager.instance.AddListener<QuerySkillListEvent> (getSkillList);
	}

	//à l'initialisation du gameObject, lance la séquence de démarrage
	public void Start(){
		Debug.Log ("ATTENTION l'addresse configurée pour l'interface web est : " + webInterfaceRootURL + " si cela ne correspond pas, décommentez la bonne version dans le GlobalManager");
		StartCoroutine(startSequence ());
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestNextSceneEvent> (nextScene);
		EventManager.instance.RemoveListener<RequestPreviousSceneEvent> (previousScene);
		EventManager.instance.RemoveListener<QueryPlayerManagerEvent> (getPlayerManager);
		EventManager.instance.RemoveListener<QuerySkillListEvent> (getSkillList);
	}

	//Séquence de démarrage, les coroutines permettent d'attendre que la méthode appelée soit entierement executées avant de yield
	//les yield sont effecuté un par un dans l'ordre
	IEnumerator startSequence(){
		yield return StartCoroutine (getSessionId ());
		if (gameSessionId == 0) {
			Debug.Log ("Le jeu n'est pas autorisé");
			yield break;
		}
		yield return StartCoroutine(pm.instanciatePlayer());
		yield return StartCoroutine (em.instanciateEnigmas ());
		if (startAtLandingPage) {
			yield return StartCoroutine (sl.loadLandingPage ());
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
		Debug.Log ("Next scene requested by " + e.currentSceneName);
		if (e.currentSceneName == "HomeScene") {
			StartCoroutine(sl.loadSkillsMenu ());
		} else if (e.currentSceneName == "SelectionScene"){
			Skill chosenSkill = em.getSkills () [e.choiceId];
			StartCoroutine (sl.loadEnigmaSequence (em.getSkills()[e.choiceId]));
		}
	}

	void previousScene(RequestPreviousSceneEvent e){
		Debug.Log ("Previous scene requested by " + e.currentSceneName);
		if (e.currentSceneName == "HomeScene") {
			//fermer le jeu?
		} else if (e.currentSceneName == "SelectionScene") {
			StartCoroutine (sl.loadLandingPage ());
		} else if (e.currentSceneName == "EnigmaSequenceScene") {
			StartCoroutine (sl.loadSkillsMenu ());
		}
	}

	void getPlayerManager(QueryPlayerManagerEvent e){
		e.playerManager = this.pm;
	}

	void getSceneLoader(QuerySceneLoaderEvent e){
		e.sceneLoader = this.sl;
	}

	void getSkillList(QuerySkillListEvent e ){
		try{
			e.skillList = em.getSkills ();
		} catch (Exception exception) {
			throw exception;
		}
	}
}