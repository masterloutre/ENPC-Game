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
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{

    public bool startAtLandingPage = false;
    public bool isOnline { get; private set; }
    private int gameSessionId;
    private PlayerManager playerManager;
	  private SceneLoader sceneLoader;
	  private EnigmaManager enigmaManager;
	  private ScoreManager scoreManager;
    private Skill currentEvaluatedSkill;

	// Variable statique : url root de l'interface web
	public static string webInterfaceRootURL {
		//VERSION KEN
		//get { return "http://localhost/enpc-web-interface"; }
		//VERSION LOU
		get { return "http://localhost:8888"; }
    //get { return "http://millenaire4.enpc.fr";}
	}

	//récupère les références au PlayerManager, au SceneLoader, à l'EnigmaManager
	//enregistre les listener d'events
	public void Awake(){

        // RÉFÉRENCES des managers stockés
		playerManager = gameObject.GetComponent<PlayerManager> ();
		sceneLoader = gameObject.GetComponent<SceneLoader> ();
		enigmaManager = gameObject.GetComponent<EnigmaManager> ();
		scoreManager = gameObject.GetComponent<ScoreManager> ();
    currentEvaluatedSkill = null;

        // LISTENERS initialisés
		    EventManager.instance.AddListener<RequestNextSceneEvent> (nextScene); // Prochain écran || MenuSceneManager.nextScene()
        EventManager.instance.AddListener<RequestPreviousSceneEvent> (previousScene); // Précédent écran || ReturnButton.PreviousScene()
        EventManager.instance.AddListener<QueryPlayerManagerEvent> (getPlayerManager); // Demande de l'instance de PlayerManager || PlayerInfoText.Awake()
        EventManager.instance.AddListener<QuerySkillListEvent>(getSkillList); // Demande de l'instance de la liste des compétences || SkillsMenuSceneManager.Start()

        EventManager.instance.AddListener<QuerySceneLoaderEvent> (getSceneLoader); // Demande de l'instance de SceneLoader || EnigmaSequenceManager.Awake()
		    EventManager.instance.AddListener<QueryEnigmaListEvent> (getEnigmaList); // Demande de l'instance de la liste des énigmes || EnigmaSequenceManager.updateEnigmaList()
        EventManager.instance.AddListener<RequestSaveScoreEvent> (saveScoreToServer); // Envoi de score au serveur || EnigmaSequenceManager.getEnigmaScore(EnigmaSubmittedEvent)
        EventManager.instance.AddListener<RequestEnigmaRemoved> (removeEnigma);
    }

    // SUPPRESSION des listeners une fois terminé
    void OnDestroy()
    {
        EventManager.instance.RemoveListener<RequestNextSceneEvent>(nextScene);
        EventManager.instance.RemoveListener<RequestPreviousSceneEvent>(previousScene);
        EventManager.instance.RemoveListener<QueryPlayerManagerEvent>(getPlayerManager);
        EventManager.instance.RemoveListener<QuerySceneLoaderEvent>(getSceneLoader);
        EventManager.instance.RemoveListener<QuerySkillListEvent>(getSkillList);
        EventManager.instance.RemoveListener<QueryEnigmaListEvent>(getEnigmaList);
        EventManager.instance.RemoveListener<RequestSaveScoreEvent>(saveScoreToServer);
        EventManager.instance.RemoveListener<RequestEnigmaRemoved> (removeEnigma);


    }

    //à l'initialisation du gameObject, lance la séquence de démarrage
    public void Start(){
		Debug.Log ("ATTENTION : l'addresse indiquée pour l'interface est : " + webInterfaceRootURL + " Si ce n'est pas la bonne il faut décommenter la bonne version dans GlobalManager");
		StartCoroutine(startSequence ());
	}

	//Séquence de démarrage, les coroutines permettent d'attendre que la méthode appelée soit entierement executées avant de yield
	//les yield sont effecuté un par un dans l'ordre
	IEnumerator startSequence(){
		yield return StartCoroutine (getSessionId ());

		yield return StartCoroutine(playerManager.instanciatePlayer());
		yield return StartCoroutine (enigmaManager.instanciateEnigmas ());
		if (startAtLandingPage) {
			yield return StartCoroutine (sceneLoader.loadLandingPage ());

		}
    if (gameSessionId == 0)
    {
      	yield return StartCoroutine (sceneLoader.loadPopUp("SessionNotAllowedPopUp"));
    }
	}

    // Récupère l'ID de la session en cours
    // si aucune session n'est ouverte, renvoie 0
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
        print("Game Session ID Parsing failed: "+ getRequest.downloadHandler.text);
				this.gameSessionId = 0;
			}
		}
	}


	/**********************
	 * GESTION DES EVENTS *
	 * ********************/

	// Load le prochain menu en fonction du nom de la scène actuelle
	//les coroutines ne sont peut etre pas forcément nécessaire mais on les garde pour pouvoir garder
	// les fct du scenenLoader avec des valeurs de retour IENumerator pour plus d'homogénéité
	void nextScene(RequestNextSceneEvent e){
		//Debug.Log ("Next scene requested by " + e.currentSceneName);
		if (e.currentSceneName == "HomeScene") {
			StartCoroutine(sceneLoader.loadSkillsMenu ());
			//StartCoroutine(sl.loadEnigma (4));
		} else if (e.currentSceneName == "SelectionScene"){
			currentEvaluatedSkill = enigmaManager.getSkills () [e.choiceId];
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

  void removeEnigma(RequestEnigmaRemoved e){
    enigmaManager.removeEnigma(e.enigma);
  }

    // GETTERs
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
      //si la skill était déja précisée dans l'event
			if(e.skill != null){
				e.enigmaList = enigmaManager.getEnigmasBySkill(e.skill);
			} else if(currentEvaluatedSkill != null){ //si elle n'était pas précisée dans l'évent mais qu'elle est connue par GlobalManager
          e.skill = currentEvaluatedSkill;
          e.enigmaList = enigmaManager.getEnigmasBySkill(e.skill);
      } else { // si il n'y a pas d'info sur la skill
          e.enigmaList = enigmaManager.getEnigmas();
      }
		} catch (Exception exception) {
			throw exception;
		}
	}

    // Envoi du score au serveur
	void saveScoreToServer(RequestSaveScoreEvent e){
		e.score.id_etudiant = playerManager.getPlayerId ();
		scoreManager.saveScoreToServer (e.score);
	}
}
