using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Component qui gère un menu
 */
public class MenuSceneManager : MonoBehaviour {
	public int choice = 0; // si c'est un menu qui a plusieurs issues

	//Demande la prochaine scène en fonction du choix qui a été fait
	public void nextScene(){
		//Traité dans GlobalManager
		EventManager.instance.Raise (new RequestNextSceneEvent(gameObject.scene.name, choice));
	}
}
