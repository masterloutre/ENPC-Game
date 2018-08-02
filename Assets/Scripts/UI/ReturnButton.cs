using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Component qui permet de retourner au menu précédent
 */ 

public class ReturnButton : MonoBehaviour {

	//Envoie un event de type RequestPreviousSceneEvent
	public void PreviousScene(){
		//Traité dans GlobalManager
		EventManager.instance.Raise (new RequestPreviousSceneEvent(SceneManager.GetActiveScene().name, 0));
	}
}
