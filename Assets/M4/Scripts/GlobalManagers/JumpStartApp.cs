using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Toute les données globales sont instanciées dans le gameObject App la scene _Preload, 
 * Ce script permet d'invoquer l'object App depuis une autre scène pour lancer le jeu depuis une autre scène en développement
 * À l'heure actuelle il ne permet pas de reloader la scène les données relatives au serveur web par exemple ne seront donc pas 
 * disponible à l'affichage directement.
 */ 

public class JumpStartApp : MonoBehaviour {
	public GameObject appPrefab;

	//Vérifie si l'object App existe et le crée s'il ce n'est pas le cas 
	//Le game Object se détruit quand il a executé sa mission ou s'il n'a pas lieu d'être
	public void Awake() {
		if (FindObjectsOfType<GlobalManager> ().Length == 0) {
			Debug.Log ("Ap needs to jump start!!!");
			Instantiate (appPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			Destroy (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
