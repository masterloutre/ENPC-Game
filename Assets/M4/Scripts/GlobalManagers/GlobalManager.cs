using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gère le déroulement du jeu
 */

public class GlobalManager : MonoBehaviour {
	private PlayerManager pm;
	private SceneLoader sl;
	private EnigmaManager em;

	//variable statique : url root de l'interface web
	public static string webInterfaceRootURL { 
		get { return "http://localhost:8888"; }
	}

	//récupère les références au PlayerManager et au SceneLoader
	public void Awake(){
		pm = gameObject.GetComponent<PlayerManager> ();
		sl = gameObject.GetComponent<SceneLoader> ();
		em = gameObject.GetComponent<EnigmaManager> ();
		EventManager.instance.AddListener<RequestNextMenuEvent> (NextMenu);
	}

	//à l'initialisation du gameObject, lance la séquence de démarrage
	public void Start(){
		StartCoroutine(startSequence ());
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<RequestNextMenuEvent> (NextMenu);
	}

	//Séquence de démarrage, les coroutines permettent d'attendre que la méthode appelée soit entierement executées avant de yield
	//les yield sont effecuté un par un dans l'ordre
	IEnumerator startSequence(){
		yield return StartCoroutine(pm.instanciatePlayer());
		yield return StartCoroutine (em.instanciateEnigmas ());
		yield return StartCoroutine(sl.loadLandingPage());
		Debug.Log ("test enigme " + em.getIdByUnityIndex (2));
		Debug.Log ("test enigme next" + em.getNextUnityIndex (2));
	}

	//load le prochain menu en fonction du nom de la scène actuelle
	//les coroutines ne sont peut etre pas forcément nécessaire mais on les garde pour pouvoir garder 
	// les fct du scenenLoader avec des valeurs de retour IENumerator pour plus d'homogénéité
	void NextMenu(RequestNextMenuEvent e){
		if (e.currentSceneName == "HomeScene") {
			StartCoroutine(sl.loadSkillsMenu ());
		}
	}

	//void NextEnigme

}