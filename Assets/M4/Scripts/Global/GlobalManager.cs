using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {
	private PlayerManager pm;
	private SceneLoader sl;

	public static string webInterfaceRootURL { 
		get { return "http://localhost:8888"; }
	}

	public void Awake(){
		pm = gameObject.GetComponent<PlayerManager> ();
		sl = gameObject.GetComponent<SceneLoader> ();
	}

	public void Start(){
		StartCoroutine(startSequence ());
	}

	IEnumerator startSequence(){
		yield return StartCoroutine(pm.instanciatePlayer());
		yield return StartCoroutine(sl.loadLandingPage());


	}
}