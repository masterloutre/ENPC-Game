using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gère le déroulement du jeu
 */

public class GlobalManager : MonoBehaviour {
	private PlayerManager pm;
	private SceneLoader sl;

	//variable statique : url root de l'interface web
	public static string webInterfaceRootURL { 
		get { return "http://localhost:8888"; }
	}

	//récupère les références au PlayerManager et au SceneLoader
	public void Awake(){
		pm = gameObject.GetComponent<PlayerManager> ();
		sl = gameObject.GetComponent<SceneLoader> ();
		EventManager.instance.AddListener<RequestNextMenuEvent> (NextMenu);
	}

	//à l'initialisation du gameObject, lance la séquence de démarrage
	public void Start(){
		StartCoroutine(startSequence ());
	}

	public void OnDestry(){
		EventManager.instance.RemoveListener<RequestNextMenuEvent> (NextMenu);
	}

	//Séquence de démarrage, les coroutines permettent d'attendre que la méthode appelée soit entierement executées avant de yield
	//les yield sont effecuté un par un dans l'ordre
	IEnumerator startSequence(){
		yield return StartCoroutine(pm.instanciatePlayer());
		yield return StartCoroutine(sl.loadLandingPage());
	}

	void NextMenu(RequestNextMenuEvent e){
		if (e.currentSceneName == "HomeScene") {
			StartCoroutine(sl.loadSkillsMenu ());
		}
	}

}